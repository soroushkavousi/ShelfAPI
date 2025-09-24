using MediatR;
using ShelfApi.Modules.IdentityModule.Contracts.Dtos;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.IdentityModule.Contracts.Commands;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}