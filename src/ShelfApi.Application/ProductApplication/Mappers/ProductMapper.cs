using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Mappers;

public class ProductMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductUserView>()
            .Map(dest => dest.Price, src => src.Price.Value);
    }
}