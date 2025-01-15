using ShelfApi.Application.AuthApplication.Dtos;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithEmail;

public class LogInWithEmailCommand : IRequest<Result<UserCredentialDto>>
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
}