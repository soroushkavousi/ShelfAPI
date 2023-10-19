using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = "User")]
[Route("app")]
public abstract class AppBaseController : ApiBaseController
{
    public AppBaseController(ISender sender) : base(sender) { }
}