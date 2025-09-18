using MediatR;
using Microsoft.AspNetCore.Identity;
using ShelfApi.Modules.Identity.Application.Services.TokenServicePack;
using ShelfApi.Modules.Identity.Contracts.Commands;
using ShelfApi.Modules.Identity.Contracts.Dtos;
using ShelfApi.Modules.Identity.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.Identity.Application.CommandHandlers;

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