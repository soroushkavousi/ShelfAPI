using Microsoft.AspNetCore.Identity;
using ShelfApi.Application.AuthApplication.Dtos;
using ShelfApi.Application.AuthApplication.Services;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Commands.LogInWithUsername;

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