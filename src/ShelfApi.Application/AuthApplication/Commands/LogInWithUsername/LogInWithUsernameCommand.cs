using ShelfApi.Application.AuthApplication.Dtos;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithUsername;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}