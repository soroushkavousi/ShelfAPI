﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Services;
using ShelfApi.Domain.Common.Exceptions;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.AuthApplication.Commands.SignUpWithEmail;

public class SignUpWithEmailCommandHandler(UserManager<User> userManager,
    IIdGenerator idGenerator) : IRequestHandler<SignUpWithEmailCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SignUpWithEmailCommand request, CancellationToken cancellationToken)
    {
        if (await userManager.Users.AnyAsync(u => u.NormalizedEmail == request.EmailAddress.ToUpper(), cancellationToken: cancellationToken))
            return new Error(ErrorCode.ItemAlreadyExists, ErrorField.Email);

        if (await userManager.Users.AnyAsync(u => u.NormalizedUserName == request.Username.ToUpper(), cancellationToken: cancellationToken))
            return new Error(ErrorCode.ItemAlreadyExists, ErrorField.Username);

        User user = new(idGenerator.GenerateId(), false, request.Username, request.EmailAddress);

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code.StartsWith("Password")))
                return new Error(ErrorCode.InvalidFormat, ErrorField.Password);
        }

        result = await userManager.AddToRoleAsync(user, RoleName.USER.ToString());
        if (!result.Succeeded)
            throw new ServerException($"Can not add role {RoleName.USER} to user {user.UserName}");

        return true;
    }
}