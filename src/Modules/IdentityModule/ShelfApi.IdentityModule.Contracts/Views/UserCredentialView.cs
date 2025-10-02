namespace ShelfApi.IdentityModule.Contracts.Views;

public record UserCredentialView
{
    public required string AccessToken { get; init; }
    public required Guid RefreshToken { get; init; }
}