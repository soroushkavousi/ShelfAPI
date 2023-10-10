namespace ShelfApi.Application.AuthApplication;

public class LogInWithEmailCommand : ApiRequest<UserCredentialDto>
{
    public string EmailAddress { get; init; }
    public string Password { get; init; }
}
