using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.Application.AuthApplication.Dtos;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Services;

public class TokenService(IBaseDataService baseDataService, UserManager<User> userManager)
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
        SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(baseDataService.JwtSettings.Key));

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: baseDataService.JwtSettings.Issuer,
            audience: baseDataService.JwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}