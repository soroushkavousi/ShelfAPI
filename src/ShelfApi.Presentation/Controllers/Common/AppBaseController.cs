using MediatR;
using Microsoft.AspNetCore.Authorization;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = nameof(RoleName.USER))]
public abstract class AppBaseController : ApiBaseController
{
    public AppBaseController(ISender sender) : base(sender) { }
}