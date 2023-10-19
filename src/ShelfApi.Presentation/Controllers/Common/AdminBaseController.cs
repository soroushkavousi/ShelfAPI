using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin")]
public abstract class AdminBaseController : ApiBaseController
{
    public AdminBaseController(ISender sender) : base(sender) { }
}