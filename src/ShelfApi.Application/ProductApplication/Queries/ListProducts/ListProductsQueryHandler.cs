using Bitiano.Shared.Services.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public class ListProductsQueryHandler(IElasticsearchService<ProductElasticDocument> productElasticsearchService,
    IShelfApiDbContext dbContext) : IRequestHandler<ListProductsQuery, Result<ProductUserView[]>>
{
    public async Task<Result<ProductUserView[]>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        bool hasFilters = !string.IsNullOrWhiteSpace(request.Name) ||
            request.MinPrice.HasValue ||
            request.MaxPrice.HasValue;

        if (hasFilters)
            return await GetProductsFromElasticsearchAsync(request, cancellationToken);

        return await GetProductsFromDatabaseAsync(cancellationToken);
    }

    private async Task<Result<ProductUserView[]>> GetProductsFromElasticsearchAsync(
        ListProductsQuery request, CancellationToken cancellationToken)
    {
        ElasticsearchResult<ProductElasticDocument[]> searchResult = await productElasticsearchService.SearchAsync(q => q
            .Bool(b =>
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    b.Must(m => m.Match(mq => mq.Field(f => f.Name).Query(request.Name)));
                }

                if (request.MinPrice.HasValue)
                {
                    b.Filter(f => f.Range(r => r.NumberRange(nr => nr
                        .Field(f => f.Price)
                        .Gte((double?)request.MinPrice)
                    )));
                }

                if (request.MaxPrice.HasValue)
                {
                    b.Filter(f => f.Range(r => r.NumberRange(nr => nr
                        .Field(f => f.Price)
                        .Lte((double?)request.MaxPrice)
                    )));
                }
            })
        );

        if (searchResult.HasError)
            return ErrorCode.InternalServerError;

        ProductElasticDocument[] productDocuments = searchResult.Data;

        ProductUserView[] products = productDocuments.Select(x => x.ToUserView()).ToArray();

        return products;
    }

    private async Task<Result<ProductUserView[]>> GetProductsFromDatabaseAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("GetProductsFromDatabaseAsync");
        Product[] products = await dbContext.Products
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        ProductUserView[] productViews = products.Select(x => x.ToUserView()).ToArray();

        return productViews;
    }
}