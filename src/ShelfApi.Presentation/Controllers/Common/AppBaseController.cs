using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = "User")]
[Route("app")]
public class AppBaseController : ApiBaseController
{

}