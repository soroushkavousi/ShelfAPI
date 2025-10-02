using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.IdentityModule.Contracts.Commands;
using ShelfApi.IdentityModule.Contracts.Views;
using ShelfApi.Presentation.Controllers.AuthController.UsernameController.Inputs;
using ShelfApi.Presentation.Controllers.Common;
using ShelfApi.Presentation.Tools;

namespace ShelfApi.Presentation.Controllers.AuthController.UsernameController;

[Route("app/auth/username")]
public class AppUsernameAuthController(ISender sender) : AppBaseController(sender)
{
    /// <summary>
    ///     Log in with username.
    /// </summary>
    /// <returns>Logged in user</returns>
    /// <response code="200">Returns the newly created item</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(Result<UserCredentialView>), StatusCodes.Status200OK)]
    [Consumes(Constants.JsonContentTypeName)]
    [HttpPost("log-in")]
    public async Task<ActionResult<Result<UserCredentialView>>> LogInWithUsernameAsync(
        [FromBody] LogInWithUsernameInputBody inputBody)
    {
        Result<UserCredentialView> result = await _sender.Send(new LogInWithUsernameCommand
        {
            Username = inputBody.Username,
            Password = inputBody.Password
        });

        return result;
    }
}