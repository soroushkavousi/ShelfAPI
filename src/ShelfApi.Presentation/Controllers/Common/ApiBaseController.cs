using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[ApiController]
[Produces(Tools.Constants.JsonContentTypeName)]
public abstract class ApiBaseController(ISender sender = null) : ControllerBase
{
    protected readonly ISender _sender = sender;
}