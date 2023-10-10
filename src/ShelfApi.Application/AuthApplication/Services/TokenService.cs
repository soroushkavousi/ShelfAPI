using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Domain.UserAggregate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShelfApi.Application.AuthApplication;

public class TokenService
{
    private readonly UserManager<User> _userManager;

    public TokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserCredentialDto> GenerateAccessTokenAsync(User user)
    {
        List<Claim> authClaims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
        };

        IList<string> userRoles = await _userManager.GetRolesAsync(user);
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

    private static string GenerateAccessToken(List<Claim> claims)
    {
        Configs.JwtConfigs jwtConfigs = Configs.Current.Jwt;
        SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Key));

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: jwtConfigs.Issuer,
            audience: jwtConfigs.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
