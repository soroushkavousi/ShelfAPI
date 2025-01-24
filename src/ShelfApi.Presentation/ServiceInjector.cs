using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ShelfApi.Application.Common.Data;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common.Tools.Serializer;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.Domain.UserAggregate;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Presentation.ActionFilters;
using ShelfApi.Presentation.Tools;
using ShelfApi.Presentation.Tools.Auth;
using ShelfApi.Presentation.Tools.Swagger;

namespace ShelfApi.Presentation;

public static class ServiceInjector
{
    public static async Task<StartupData> ReadStartupData()
    {
        ShelfApiDbContext shelfApiDbContext = new(EnvironmentVariables.ConnectionString);
        try
        {
            string startupDataJson = await shelfApiDbContext.ProjectSettings
                .Where(x => x.Id == ProjectSettingId.StartupData)
                .Select(x => x.Data)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(startupDataJson))
            {
                Log.Fatal("Error: Please provide startup data!");
                return StartupData.Default;
            }

            StartupData startupData = startupDataJson.FromJson<StartupData>();
            startupData.DbConnectionString = EnvironmentVariables.ConnectionString;
            return startupData;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Could not load ProjectSettings because of {ErrorName} {ErrorMessage}",
                ex.GetType().Name, ex.Message);
            return StartupData.Default;
        }
        finally
        {
            await shelfApiDbContext.DisposeAsync();
        }
    }

    public static void AddPresentation(this IServiceCollection services, StartupData startupData)
    {
        services.AddControllers(o =>
            {
                o.Filters.Add<StatusCodeActionFilter>();
            })
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonNumberEnumConverter<ErrorCode>());
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.DocumentFilter<SwaggerDocumentFilter>();
        });

        services.AddAuth(startupData);
    }

    private static void AddAuth(this IServiceCollection services, StartupData startupData)
    {
        services.AddIdentity();
        services.AddAuthentication(startupData);
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

    private static void AddAuthentication(this IServiceCollection services, StartupData startupData)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = startupData.JwtIssuer,
                ValidAudience = startupData.JwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(startupData.JwtKey))
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