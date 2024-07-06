using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.Common.Exceptions;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication;

public class SignUpWithEmailCommandHandler : IRequestHandler<SignUpWithEmailCommand, Result<bool>>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdManager _idManager;

    public SignUpWithEmailCommandHandler(UserManager<User> userManager, IIdManager idManager)
    {
        _userManager = userManager;
        _idManager = idManager;
    }

    public async Task<Result<bool>> Handle(SignUpWithEmailCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(u => u.NormalizedEmail == request.EmailAddress.ToUpper(), cancellationToken: cancellationToken))
            return new Error(ErrorCode.ItemAlreadyExists, ErrorField.Email);

        if (await _userManager.Users.AnyAsync(u => u.NormalizedUserName == request.Username.ToUpper(), cancellationToken: cancellationToken))
            return new Error(ErrorCode.ItemAlreadyExists, ErrorField.Username);

        ulong userId = _idManager.GenerateNextUlong();
        User user = new User(userId, false, request.Username, request.EmailAddress);

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code.StartsWith("Password")))
                return new Error(ErrorCode.InvalidFormat, ErrorField.Password);
        }

        result = await _userManager.AddToRoleAsync(user, RoleName.USER.ToString());
        if (!result.Succeeded)
            throw new ServerException($"Can not add role {RoleName.USER} to user {user.UserName}");

        return true;
    }
}