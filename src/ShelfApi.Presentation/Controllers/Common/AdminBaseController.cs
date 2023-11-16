using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = nameof(RoleName.ADMIN))]
[Route("admin")]
public abstract class AdminBaseController : ApiBaseController
{
    public AdminBaseController(ISender sender) : base(sender) { }
}