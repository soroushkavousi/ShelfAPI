using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.Application.AuthApplication.Dtos;
using ShelfApi.Application.AuthApplication.ValueObjects;
using ShelfApi.Application.Common.Data;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Services;

public class TokenService(StartupData startupData, UserManager<User> userManager)
{
    public async Task<UserCredentialDto> GenerateAccessTokenAsync(User user)
    {
        List<Claim> authClaims =
        [
            new(ClaimNames.UserId, user.Id.ToString())
        ];

        IList<string> userRoles = await userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(x => new Claim(ClaimNames.Roles, x)));

        string accessToken = GenerateAccessToken(authClaims);
        Guid refreshToken = Guid.NewGuid();

        UserCredentialDto userCredential = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
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