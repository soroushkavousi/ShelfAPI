using DotNetPotion.AppEnvironmentPack;
using Microsoft.AspNetCore.Mvc;

namespace ShelfApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AAAController : ControllerBase
{
    [HttpPost("test")]
    public async Task<IActionResult> TestAsync([FromBody] int a1)
    {
        if (!AppEnvironment.IsDevelopment)
            return NotFound();

        await Task.CompletedTask;
        return Ok();
    }
}