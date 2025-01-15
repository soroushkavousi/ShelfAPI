namespace ShelfApi.Application.AuthApplication.Dtos;

public record UserCredentialDto
{
    public string AccessToken { get; init; }
    public Guid RefreshToken { get; init; }
}