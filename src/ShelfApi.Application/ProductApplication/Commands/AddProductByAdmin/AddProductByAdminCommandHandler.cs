﻿using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommandHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = new(request.Name, price, request.Quantity);

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }
}