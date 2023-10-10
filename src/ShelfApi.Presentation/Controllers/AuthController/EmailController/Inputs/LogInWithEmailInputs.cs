using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers;

public class LogInWithEmailInputBody
{
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Password { get; set; }
}
