using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.AuthApplication;

namespace ShelfApi.Presentation.Controllers;

[Route("app/auth/email")]
public class AppEmailAuthController(ISender sender) : AppBaseController(sender)
{
    /// <summary>
    /// Sign up with email.
    /// </summary>
    /// <returns>Created user</returns>
    /// <response code="200">Returns the newly created item</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [Consumes(Tools.Constants.JsonContentTypeName)]
    [HttpPost("sign-up")]
    public async Task<ActionResult<Result<bool>>> SignUpWithEmailAsync(
        [FromBody] SignUpWithEmailInputBody inputBody)
    {
        Result<bool> result = await _sender.Send(new SignUpWithEmailCommand
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
    [ProducesResponseType(typeof(Result<UserCredentialDto>), StatusCodes.Status200OK)]
    [Consumes(Tools.Constants.JsonContentTypeName)]
    [HttpPost("log-in")]
    public async Task<ActionResult<Result<UserCredentialDto>>> LogInWithEmailAsync(
        [FromBody] LogInWithEmailInputBody inputBody)
    {
        Result<UserCredentialDto> result = await _sender.Send(new LogInWithEmailCommand
        {
            EmailAddress = inputBody.EmailAddress,
            Password = inputBody.Password
        });

        return result;
    }
}