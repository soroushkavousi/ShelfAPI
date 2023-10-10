using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.ErrorApplication;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class SignUpWithEmailCommandHandler : ApiRequestHandler<SignUpWithEmailCommand, bool>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdManager _idManager;

    public SignUpWithEmailCommandHandler(UserManager<User> userManager, IIdManager idManager)
    {
        _userManager = userManager;
        _idManager = idManager;
    }

    protected override async Task ValidateAsync(SignUpWithEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(u => u.NormalizedEmail == request.EmailAddress.ToUpper(), cancellationToken: cancellationToken))
            throw new ALreadyExistsException(ErrorField.EMAIL, request.EmailAddress);

        if (await _userManager.Users.AnyAsync(u => u.NormalizedUserName == request.Username.ToUpper(), cancellationToken: cancellationToken))
            throw new ALreadyExistsException(ErrorField.USERNAME, request.Username);
    }

    protected override async Task<bool> OperateAsync(SignUpWithEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = _idManager.GenerateNewId();
        var user = new User(userId, false, request.Username, request.EmailAddress);

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code.StartsWith("Password")))
                throw new InvalidFormatException(ErrorField.PASSWORD);
        }

        result = await _userManager.AddToRoleAsync(user, RoleName.USER.ToString());
        if (!result.Succeeded)
            throw new InternalServerException($"Can not add role {RoleName.USER} to user {user.UserName}");

        return true;
    }
}

