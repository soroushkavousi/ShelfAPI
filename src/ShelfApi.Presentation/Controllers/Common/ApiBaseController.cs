using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Presentation.Tools;

namespace ShelfApi.Presentation.Controllers.Common;

[ApiController]
[Produces(Constants.JsonContentTypeName)]
public abstract class ApiBaseController(ISender sender = null) : ControllerBase
{
    protected readonly ISender _sender = sender;
}