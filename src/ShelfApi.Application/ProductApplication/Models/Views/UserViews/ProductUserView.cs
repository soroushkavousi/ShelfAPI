using System.Linq.Expressions;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Models.Views.UserViews;

public record ProductUserView
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }

    public static Expression<Func<Product, ProductUserView>> FromProductExpr =>
        product => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price.Value,
            Quantity = product.Quantity,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt
        };

    public static Expression<Func<ProductElasticDocument, ProductUserView>> FromProductElasticDocumentExpr =>
        productElasticDocument => new()
        {
            Id = productElasticDocument.Id,
            Name = productElasticDocument.Name,
            Price = productElasticDocument.Price,
            Quantity = productElasticDocument.Quantity,
            CreatedAt = productElasticDocument.CreatedAt,
            ModifiedAt = productElasticDocument.ModifiedAt
        };
}

public static class ProductUserViewExtensions
{
    private static readonly Func<Product, ProductUserView> _fromProduct
        = ProductUserView.FromProductExpr.Compile();

    private static readonly Func<ProductElasticDocument, ProductUserView> _fromProductElasticDocument
        = ProductUserView.FromProductElasticDocumentExpr.Compile();

    public static ProductUserView ToUserView(this Product product)
        => _fromProduct(product);

    public static ProductUserView ToUserView(this ProductElasticDocument productElasticDocument)
        => _fromProductElasticDocument(productElasticDocument);
}