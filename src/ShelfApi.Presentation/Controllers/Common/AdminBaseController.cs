using MediatR;
using Microsoft.AspNetCore.Authorization;
using ShelfApi.IdentityModule.Domain;

namespace ShelfApi.Presentation.Controllers.Common;

[Authorize(Roles = nameof(RoleName.ADMIN))]
public abstract class AdminBaseController(ISender sender) : ApiBaseController(sender)
{
}