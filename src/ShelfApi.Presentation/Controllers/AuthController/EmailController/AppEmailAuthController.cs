using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.Common;

namespace ShelfApi.Presentation.Controllers;

[Route("app/auth/email")]
public class AppEmailAuthController : AppBaseController
{
    public AppEmailAuthController(ISender sender) : base(sender) { }

    /// <summary>
    /// Sign up with email.
    /// </summary>
    /// <returns>Created user</returns>
    /// <response code="200">Returns the newly created item</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<bool>), StatusCodes.Status200OK)]
    [Consumes(Tools.Constants.JsonContentTypeName)]
    [HttpPost("sign-up")]
    public async Task<ActionResult<ResultDto<bool>>> SignUpWithEmailAsync(
        [FromBody] SignUpWithEmailInputBody inputBody)
    {
        var result = await _sender.Send(new SignUpWithEmailCommand
        {
            EmailAddress = inputBody.EmailAddress,
            Username = inputBody.Username,
            Password = inputBody.Password
        });

        return result;
    }

    /// <summary>
    /// Log in with email.
    /// </summary>
    /// <returns>Logged in user</returns>
    /// <response code="200">Returns the newly created item</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<UserCredentialDto>), StatusCodes.Status200OK)]
    [Consumes(Tools.Constants.JsonContentTypeName)]
    [HttpPost("log-in")]
    public async Task<ActionResult<ResultDto<UserCredentialDto>>> LogInWithEmailAsync(
        [FromBody] LogInWithEmailInputBody inputBody)
    {
        var result = await _sender.Send(new LogInWithEmailCommand
        {
            EmailAddress = inputBody.EmailAddress,
            Password = inputBody.Password
        });

        return result;
    }
}
