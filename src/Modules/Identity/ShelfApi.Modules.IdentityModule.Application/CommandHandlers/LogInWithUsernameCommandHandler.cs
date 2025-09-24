using MediatR;
using Microsoft.AspNetCore.Identity;
using ShelfApi.Modules.IdentityModule.Application.Services.TokenServicePack;
using ShelfApi.Modules.IdentityModule.Contracts.Commands;
using ShelfApi.Modules.IdentityModule.Contracts.Dtos;
using ShelfApi.Modules.IdentityModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.IdentityModule.Application.CommandHandlers;

public class LogInWithUsernameCommandHandler(UserManager<User> userManager, TokenService tokenService)
    : IRequestHandler<LogInWithUsernameCommand, Result<UserCredentialDto>>
{
    public async Task<Result<UserCredentialDto>> Handle(LogInWithUsernameCommand request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByNameAsync(request.Username);
        if (user is null)
            return ErrorCode.AuthenticationError;

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return ErrorCode.AuthenticationError;

        UserCredentialDto userCredential = await tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}