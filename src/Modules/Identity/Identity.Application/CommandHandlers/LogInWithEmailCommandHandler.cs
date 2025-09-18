using MediatR;
using Microsoft.AspNetCore.Identity;
using ShelfApi.Modules.Identity.Application.Services.TokenServicePack;
using ShelfApi.Modules.Identity.Contracts.Commands;
using ShelfApi.Modules.Identity.Contracts.Dtos;
using ShelfApi.Modules.Identity.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.Identity.Application.CommandHandlers;

public class LogInWithEmailCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, TokenService tokenService)
    : IRequestHandler<LogInWithEmailCommand, Result<UserCredentialDto>>
{
    public async Task<Result<UserCredentialDto>> Handle(LogInWithEmailCommand request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByEmailAsync(request.EmailAddress);
        if (user is null)
            return ErrorCode.AuthenticationError;

        SignInResult signInResult = await signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!signInResult.Succeeded)
            return ErrorCode.AuthenticationError;

        UserCredentialDto userCredential = await tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}