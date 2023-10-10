using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin")]
public class AdminBaseController : ApiBaseController
{

}