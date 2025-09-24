using MediatR;
using Microsoft.AspNetCore.Authorization;
using ShelfApi.Modules.IdentityModule.Application.ValueObjects;
using ShelfApi.Modules.IdentityModule.Domain;

namespace ShelfApi.Presentation.Controllers.Common;

[Authorize(Roles = nameof(RoleName.USER))]
public abstract class AppBaseController(ISender sender) : ApiBaseController(sender)
{
    protected int UserId => int.Parse(User.FindFirst(ClaimNames.UserId)?.Value
        ?? throw new UnauthorizedAccessException("User ID not found"));
}