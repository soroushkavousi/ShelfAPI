using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = nameof(RoleName.USER))]
[Route("app")]
public abstract class AppBaseController : ApiBaseController
{
    public AppBaseController(ISender sender) : base(sender) { }
}