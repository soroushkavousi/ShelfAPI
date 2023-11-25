using Microsoft.AspNetCore.Identity;
using ShelfApi.Application.ErrorApplication;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class LogInWithEmailCommandHandler : ApiRequestHandler<LogInWithEmailCommand, UserCredentialDto>
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public LogInWithEmailCommandHandler(UserManager<User> userManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    protected override async Task<UserCredentialDto> OperateAsync(LogInWithEmailCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.EmailAddress)
            ?? throw new NotExistException(ErrorField.EMAIL, request.EmailAddress);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new InvalidValueException(ErrorField.PASSWORD);

        var userCredential = await _tokenService.GenerateAccessTokenAsync(user);
        return userCredential;
    }
}

