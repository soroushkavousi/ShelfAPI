using System.Linq.Expressions;
using ShelfApi.Modules.ProductModule.Application.Models.Dtos.Elasticsearch;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Modules.ProductModule.Domain;

namespace ShelfApi.Modules.ProductModule.Application.Mappers;

public static class ProductMapper
{
    public static Expression<Func<Product, ProductUserView>> ProductToUserViewExpr =>
        product => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price.Value,
            Quantity = product.Quantity,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt
        };

    private static readonly Func<Product, ProductUserView> _productToUserView
        = ProductToUserViewExpr.Compile();

    public static Expression<Func<ProductElasticDocument, ProductUserView>> ProductElasticDocumentToUserViewExpr =>
        productElasticDocument => new()
        {
            Id = productElasticDocument.Id,
            Name = productElasticDocument.Name,
            Price = productElasticDocument.Price,
            Quantity = productElasticDocument.Quantity,
            CreatedAt = productElasticDocument.CreatedAt,
            ModifiedAt = productElasticDocument.ModifiedAt
        };

    private static readonly Func<ProductElasticDocument, ProductUserView> _productElasticDocumentToUserView
        = ProductElasticDocumentToUserViewExpr.Compile();

    public static ProductUserView ToUserView(this Product product)
        => _productToUserView(product);

    public static ProductUserView ToUserView(this ProductElasticDocument productElasticDocument)
        => _productElasticDocumentToUserView(productElasticDocument);
}