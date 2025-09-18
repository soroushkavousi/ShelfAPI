namespace ShelfApi.Modules.Identity.Contracts.Dtos;

public record UserCredentialDto
{
    public required string AccessToken { get; init; }
    public required Guid RefreshToken { get; init; }
}