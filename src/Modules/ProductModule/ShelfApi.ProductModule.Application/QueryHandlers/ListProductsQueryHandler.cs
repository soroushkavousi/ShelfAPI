using Elastic.Clients.Elasticsearch;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Mappers;
using ShelfApi.ProductModule.Application.Models.Dtos.Elasticsearch;
using ShelfApi.ProductModule.Contracts.Queries;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;
using ShelfApi.Shared.Elasticsearch;

namespace ShelfApi.ProductModule.Application.QueryHandlers;

public class ListProductsQueryHandler(IElasticsearchService<ProductElasticDocument> productElasticsearchService,
    IProductDbContext dbContext) : IRequestHandler<ListProductsQuery, Result<ProductUserView[]>>
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
                b.Filter(b => b.Term(t => t.Field(f => f.IsDeleted).Value(false)));

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

        IQueryable<Product> query = dbContext.Products
            .Where(x => !x.IsDeleted);

        query = request.SortDescending
            ? query.OrderByDescending(x => x.CreatedAt)
            : query.OrderBy(x => x.CreatedAt);

        ProductUserView[] productUserViews = await query
            .Skip(pagination.From)
            .Take(pagination.PageSize)
            .AsNoTracking()
            .Select(ProductMapper.ProductToUserViewExpr)
            .ToArrayAsync(cancellationToken);

        return new(productUserViews, pagination);
    }
}