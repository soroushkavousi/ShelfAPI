using ShelfApi.Domain.FinancialAggregate;

namespace ShelfApi.Application.ProductApplication;

public class AddProductByAdminCommand : ApiRequest<ProductDto>
{
    public string Name { get; set; }
    public Price Price { get; set; }
    public int Quantity { get; set; }
}
