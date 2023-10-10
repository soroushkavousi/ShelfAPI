using Microsoft.AspNetCore.Identity;
using ShelfApi.Application.ErrorApplication;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class LogInWithEmailCommandHandler : ApiRequestHandler<LogInWithEmailCommand, UserCredentialDto>
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    private User _user;

    public LogInWithEmailCommandHandler(UserManager<User> userManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    protected override async Task ValidateAsync(LogInWithEmailCommand request, CancellationToken cancellationToken)
    {
        _user = await _userManager.FindByEmailAsync(request.EmailAddress)
            ?? throw new NotExistException(ErrorField.EMAIL, request.EmailAddress);

        if (!await _userManager.CheckPasswordAsync(_user, request.Password))
            throw new InvalidValueException(ErrorField.PASSWORD);
    }

    protected override async Task<UserCredentialDto> OperateAsync(LogInWithEmailCommand request, CancellationToken cancellationToken)
    {
        var userCredential = await _tokenService.GenerateAccessTokenAsync(_user);
        return userCredential;
    }
}

