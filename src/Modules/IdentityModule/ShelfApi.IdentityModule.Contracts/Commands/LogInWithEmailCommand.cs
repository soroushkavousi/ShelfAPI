using MediatR;
using ShelfApi.IdentityModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Contracts.Commands;

public class LogInWithEmailCommand : IRequest<Result<UserCredentialView>>
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
}