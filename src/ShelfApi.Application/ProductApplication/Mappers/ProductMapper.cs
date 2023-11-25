using Mapster;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication;

public class ProductMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Price, src => src.Price.Value);
    }
}
