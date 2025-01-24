using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.Application.AuthApplication.Dtos;
using ShelfApi.Application.Common.Data;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Services;

public class TokenService(StartupData startupData, UserManager<User> userManager)
{
    public async Task<UserCredentialDto> GenerateAccessTokenAsync(User user)
    {
        List<Claim> authClaims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName)
        ];

        IList<string> userRoles = await userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

        string accessToken = GenerateAccessToken(authClaims);
        Guid refereshToken = Guid.NewGuid();

        UserCredentialDto userCredential = new UserCredentialDto
        {
            AccessToken = accessToken,
            RefreshToken = refereshToken
        };

        return userCredential;
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(startupData.JwtKey));

        JwtSecurityToken jwtToken = new(
            issuer: startupData.JwtIssuer,
            audience: startupData.JwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: new(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}