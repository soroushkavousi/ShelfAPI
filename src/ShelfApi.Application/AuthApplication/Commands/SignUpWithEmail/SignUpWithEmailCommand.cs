namespace ShelfApi.Application.AuthApplication;

public class SignUpWithEmailCommand : ApiRequest<bool>
{
    public string EmailAddress { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}
