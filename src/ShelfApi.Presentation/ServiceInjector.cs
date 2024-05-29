using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.UserAggregate;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Presentation.Tools;
using System.Text;

namespace ShelfApi.Presentation;

public static class ServiceInjector
{
    public static void AddPresentation(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuth(jwtSettings);
    }

    private static void AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddIdentity();
        services.AddAuthentication(jwtSettings);
        services.AddAuthorization();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = User.AllowedUserNameCharacters;
        })
        .AddEntityFrameworkStores<ShelfApiDbContext>()
        .AddDefaultTokenProviders();
    }

    private static void AddAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
    }

    private static void AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
        });

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, ApiAuthorizationMiddlewareResultHandler>();
    }
}