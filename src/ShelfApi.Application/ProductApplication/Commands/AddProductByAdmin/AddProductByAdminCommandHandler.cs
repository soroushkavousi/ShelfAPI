using Bitiano.Shared.Services.Elasticsearch;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommandHandler(IShelfApiDbContext dbContext,
    IElasticsearchService<ProductElasticDocument> productElasticsearchService)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductUserView>>
{
    public async Task<Result<ProductUserView>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = new(request.Name, price, request.Quantity);

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

        await productElasticsearchService.AddOrUpdateAsync(product.ToElasticDocument());

        return product.Adapt<ProductUserView>();
    }
}