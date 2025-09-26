using MediatR;
using Microsoft.AspNetCore.Identity;
using ShelfApi.IdentityModule.Application.Services.TokenServicePack;
using ShelfApi.IdentityModule.Contracts.Commands;
using ShelfApi.IdentityModule.Contracts.Dtos;
using ShelfApi.IdentityModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.IdentityModule.Application.CommandHandlers;

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