using MediatR;
using ShelfApi.IdentityModule.Contracts.Dtos;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Contracts.Commands;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialDto>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}