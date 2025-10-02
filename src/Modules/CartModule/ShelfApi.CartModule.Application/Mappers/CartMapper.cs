using System.Linq.Expressions;
using ShelfApi.CartModule.Contracts.Views;
using ShelfApi.CartModule.Domain;

namespace ShelfApi.CartModule.Application.Mappers;

public static class CartMapper
{
    public static Expression<Func<Cart, CartUserView>> CartToViewExpr =>
        Cart => new()
        {
            UserId = Cart.UserId,
            Name = Cart.Name,
            Price = Cart.Price.Value,
            Quantity = Cart.Quantity,
            CreatedAt = Cart.CreatedAt,
            ModifiedAt = Cart.ModifiedAt
        };

    private static readonly Func<Cart, CartUserView> _CartToUserView
        = CartToUserViewExpr.Compile();

    public static Expression<Func<CartElasticDocument, CartUserView>> CartElasticDocumentToUserViewExpr =>
        CartElasticDocument => new()
        {
            Id = CartElasticDocument.Id,
            Name = CartElasticDocument.Name,
            Price = CartElasticDocument.Price,
            Quantity = CartElasticDocument.Quantity,
            CreatedAt = CartElasticDocument.CreatedAt,
            ModifiedAt = CartElasticDocument.ModifiedAt
        };

    private static readonly Func<CartElasticDocument, CartUserView> _CartElasticDocumentToUserView
        = CartElasticDocumentToUserViewExpr.Compile();

    public static CartUserView ToUserView(this Cart Cart)
        => _CartToUserView(Cart);

    public static CartUserView ToUserView(this CartElasticDocument CartElasticDocument)
        => _CartElasticDocumentToUserView(CartElasticDocument);
}