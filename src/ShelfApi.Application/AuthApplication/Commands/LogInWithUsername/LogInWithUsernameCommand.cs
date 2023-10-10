namespace ShelfApi.Application.AuthApplication;

public class LogInWithUsernameCommand : ApiRequest<UserCredentialDto>
{
    public string Username { get; init; }
    public string Password { get; init; }
}
