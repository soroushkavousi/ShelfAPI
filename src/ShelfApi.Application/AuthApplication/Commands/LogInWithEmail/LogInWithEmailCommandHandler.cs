using Microsoft.AspNetCore.Identity;
using ShelfApi.Application.AuthApplication.Dtos;
using ShelfApi.Application.AuthApplication.Services;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithEmail;

public class LogInWithEmailCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, TokenService tokenService)
    : IRequestHandler<LogInWithEmailCommand, Result<UserCredentialDto>>
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly TokenService _tokenService = tokenService;

    public async Task<Result<UserCredentialDto>> Handle(LogInWithEmailCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.EmailAddress);
        if (user is null)
            return ErrorCode.AuthenticationError;

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!signInResult.Succeeded)
            return ErrorCode.AuthenticationError;

        UserCredentialDto userCredential = await _tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}