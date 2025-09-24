namespace ShelfApi.Modules.IdentityModule.Application.Services.TokenServicePack;

public class TokenServiceOptions
{
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
}