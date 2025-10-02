using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.IdentityModule.Application.ValueObjects;
using ShelfApi.IdentityModule.Contracts.Views;
using ShelfApi.IdentityModule.Domain;

namespace ShelfApi.IdentityModule.Application.Services.TokenServicePack;

public class TokenService(IOptions<TokenServiceOptions> options, UserManager<User> userManager)
{
    private readonly TokenServiceOptions _options = options.Value;

    public async Task<UserCredentialView> GenerateAccessTokenAsync(User user)
    {
        List<Claim> authClaims =
        [
            new(ClaimNames.UserId, user.Id.ToString())
        ];

        IList<string> userRoles = await userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(x => new Claim(ClaimNames.Roles, x)));

        string accessToken = GenerateAccessToken(authClaims);
        Guid refreshToken = Guid.NewGuid();

        UserCredentialView userCredential = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return userCredential;
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_options.JwtKey));

        JwtSecurityToken jwtToken = new(
            issuer: _options.JwtIssuer,
            audience: _options.JwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: new(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}