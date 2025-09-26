using MediatR;
using ShelfApi.IdentityModule.Contracts.Dtos;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Contracts.Commands;

public class LogInWithEmailCommand : IRequest<Result<UserCredentialDto>>
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
}