using ShelfApi.Application.AuthApplication.Dtos;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithUsername;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public string Username { get; init; }
    public string Password { get; init; }
}