using MediatR;
using ShelfApi.IdentityModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Contracts.Commands;

public class LogInWithUsernameCommand : IRequest<Result<UserCredentialView>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}