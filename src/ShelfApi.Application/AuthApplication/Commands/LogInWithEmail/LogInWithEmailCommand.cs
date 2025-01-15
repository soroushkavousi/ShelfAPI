using ShelfApi.Application.AuthApplication.Dtos;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithEmail;

public class LogInWithEmailCommand : IRequest<Result<UserCredentialDto>>
{
    public string EmailAddress { get; init; }
    public string Password { get; init; }
}