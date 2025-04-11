using Bitiano.Shared.Services.Elasticsearch;
using Bitiano.Shared.ValueObjects;
using Elastic.Clients.Elasticsearch;
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

        return await GetProductsFromDatabaseAsync(request, cancellationToken);
    }

    private async Task<Result<ProductUserView[]>> GetProductsFromElasticsearchAsync(
        ListProductsQuery request, CancellationToken cancellationToken)
    {
        ElasticsearchResult<ProductElasticDocument[]> searchResult = await productElasticsearchService.SearchAsync(
            q => q.Bool(b =>
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
            }),
            sort => sort
                .Field(f => f.CreatedAt, f => f.Order(request.SortDescending ? SortOrder.Desc : SortOrder.Asc)),
            request.PageNumber,
            request.PageSize
        );

        if (searchResult.HasError)
            return ErrorCode.InternalServerError;

        ProductElasticDocument[] productDocuments = searchResult.Data;
        ProductUserView[] productUserViews = productDocuments.Select(x => x.ToUserView()).ToArray();
        return new(productUserViews, new(request.PageNumber, request.PageSize));
    }

    private async Task<Result<ProductUserView[]>> GetProductsFromDatabaseAsync(
        ListProductsQuery request, CancellationToken cancellationToken)
    {
        Pagination pagination = new(request.PageNumber, request.PageSize);

        IQueryable<Product> query = dbContext.Products;

        query = request.SortDescending
            ? query.OrderByDescending(x => x.CreatedAt)
            : query.OrderBy(x => x.CreatedAt);

        Product[] products = await query
            .Skip(pagination.From)
            .Take(pagination.PageSize)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        ProductUserView[] productUserViews = products.Select(x => x.ToUserView()).ToArray();
        return new(productUserViews, pagination);
    }
}