namespace ShelfApi.Application.AuthApplication;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public string Username { get; init; }
    public string Password { get; init; }
}