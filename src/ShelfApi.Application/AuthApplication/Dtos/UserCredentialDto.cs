namespace ShelfApi.Application.AuthApplication;

public record UserCredentialDto
{
    public string AccessToken { get; init; }
    public Guid RefreshToken { get; init; }
}
