using MediatR;
using Microsoft.AspNetCore.Identity;
using ShelfApi.IdentityModule.Application.Services.TokenServicePack;
using ShelfApi.IdentityModule.Contracts.Commands;
using ShelfApi.IdentityModule.Contracts.Views;
using ShelfApi.IdentityModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Application.CommandHandlers;

public class LogInWithUsernameCommandHandler(UserManager<User> userManager, TokenService tokenService)
    : IRequestHandler<LogInWithUsernameCommand, Result<UserCredentialView>>
{
    public async Task<Result<UserCredentialView>> Handle(LogInWithUsernameCommand request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByNameAsync(request.Username);
        if (user is null)
            return ErrorCode.AuthenticationError;

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return ErrorCode.AuthenticationError;

        UserCredentialView userCredential = await tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}