using ShelfApi.Application.ProductApplication.Models.Views.UserViews;

namespace ShelfApi.Application.ProductApplication.Queries.GetProduct;

public class GetProductQuery : IRequest<Result<ProductUserView>>
{
    public required long Id { get; init; }
}