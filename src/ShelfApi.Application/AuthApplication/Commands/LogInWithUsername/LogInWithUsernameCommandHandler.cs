using Microsoft.AspNetCore.Identity;
using ShelfApi.Application.ErrorApplication;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class LogInWithUsernameCommandHandler : ApiRequestHandler<LogInWithUsernameCommand, UserCredentialDto>
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public LogInWithUsernameCommandHandler(UserManager<User> userManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    protected override async Task<UserCredentialDto> OperateAsync(LogInWithUsernameCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByNameAsync(request.Username)
            ?? throw new NotExistException(ErrorField.USERNAME, request.Username);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new InvalidValueException(ErrorField.PASSWORD);

        var userCredential = await _tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}

