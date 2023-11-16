using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.ProductAggregate;

public class Product : BaseModel<ulong>
{
    public Product(ulong id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}