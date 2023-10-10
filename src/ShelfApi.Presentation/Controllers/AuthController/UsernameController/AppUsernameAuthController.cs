using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.Common;

namespace ShelfApi.Presentation.Controllers;

[Route("auth/username")]
public class AppUsernameAuthController : AppBaseController
{
    /// <summary>
    /// Log in with username.
    /// </summary>
    /// <returns>Logged in user</returns>
    /// <response code="200">Returns the newly created item</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<UserCredentialDto>), StatusCodes.Status200OK)]
    [Consumes(Tools.Constants.JsonContentTypeName)]
    [HttpPost("log-in")]
    public async Task<ActionResult<ResultDto<UserCredentialDto>>> LogInWithUsernameAsync(
        [FromBody] LogInWithUsernameInputBody inputBody)
    {
        var result = await _sender.Send(new LogInWithUsernameCommand
        {
            Username = inputBody.Username,
            Password = inputBody.Password
        });

        return result;
    }
}
