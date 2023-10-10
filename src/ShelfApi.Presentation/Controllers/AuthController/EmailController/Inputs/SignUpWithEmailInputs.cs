using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers;

public class SignUpWithEmailInputBody
{
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
