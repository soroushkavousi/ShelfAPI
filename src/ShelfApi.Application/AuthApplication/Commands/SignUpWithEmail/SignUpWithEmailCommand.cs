namespace ShelfApi.Application.AuthApplication.Commands.SignUpWithEmail;

public class SignUpWithEmailCommand : IRequest<Result<bool>>
{
    public required string EmailAddress { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}