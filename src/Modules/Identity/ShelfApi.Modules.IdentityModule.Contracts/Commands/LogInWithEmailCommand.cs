using MediatR;
using ShelfApi.Modules.IdentityModule.Contracts.Dtos;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.IdentityModule.Contracts.Commands;

public class LogInWithEmailCommand : IRequest<Result<UserCredentialDto>>
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
}