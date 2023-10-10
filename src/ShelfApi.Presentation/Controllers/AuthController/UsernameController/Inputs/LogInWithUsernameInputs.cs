using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers;

public class LogInWithUsernameInputBody
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
