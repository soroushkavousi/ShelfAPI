using MediatR;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.Identity.Contracts.Commands;

public class SignUpWithEmailCommand : IRequest<Result<bool>>
{
    public required string EmailAddress { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}