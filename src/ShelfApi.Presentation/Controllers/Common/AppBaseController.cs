using MediatR;
using Microsoft.AspNetCore.Authorization;
using ShelfApi.Application.AuthApplication.ValueObjects;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Presentation.Controllers.Common;

[Authorize(Roles = nameof(RoleName.USER))]
public abstract class AppBaseController(ISender sender) : ApiBaseController(sender)
{
    protected int UserId => int.Parse(User.FindFirst(ClaimNames.UserId)?.Value
        ?? throw new UnauthorizedAccessException("User ID not found"));
}