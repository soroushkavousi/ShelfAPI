using MediatR;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Application.Queries;

public class GetProductQuery : IRequest<Product>
{
    public required long Id { get; init; }
}