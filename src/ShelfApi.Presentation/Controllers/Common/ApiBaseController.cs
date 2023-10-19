using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[ApiController]
[Produces(Tools.Constants.JsonContentTypeName)]
public abstract class ApiBaseController : ControllerBase
{
    protected readonly ISender _sender;

    public ApiBaseController(ISender sender = null)
    {
        _sender = sender;
    }
}