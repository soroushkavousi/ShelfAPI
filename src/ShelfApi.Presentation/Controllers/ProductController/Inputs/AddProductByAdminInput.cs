using System.ComponentModel.DataAnnotations;

namespace ShelfApi.Presentation.Controllers;

public class AddProductByAdminInput
{
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
