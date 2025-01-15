using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers.AuthController.UsernameController.Inputs;

public class LogInWithUsernameInputBody
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}