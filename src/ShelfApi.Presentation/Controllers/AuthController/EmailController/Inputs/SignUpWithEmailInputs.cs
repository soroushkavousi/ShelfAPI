using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers.AuthController.EmailController.Inputs;

public class SignUpWithEmailInputBody
{
    [Required]
    public string EmailAddress { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}