using Microsoft.AspNetCore.Identity;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class LogInWithUsernameCommandHandler(UserManager<User> userManager, TokenService tokenService)
    : IRequestHandler<LogInWithUsernameCommand, Result<UserCredentialDto>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly TokenService _tokenService = tokenService;

    public async Task<Result<UserCredentialDto>> Handle(LogInWithUsernameCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
            return ErrorCode.AuthenticationError;

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return ErrorCode.AuthenticationError;

        UserCredentialDto userCredential = await _tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}