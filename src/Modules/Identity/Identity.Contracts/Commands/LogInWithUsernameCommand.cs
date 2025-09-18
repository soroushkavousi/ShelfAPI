using MediatR;
using ShelfApi.Modules.Identity.Contracts.Dtos;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.Identity.Contracts.Commands;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}