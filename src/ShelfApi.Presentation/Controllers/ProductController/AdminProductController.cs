﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;
using ShelfApi.Application.ProductApplication.Commands.DeleteProductByAdmin;
using ShelfApi.Application.ProductApplication.Commands.UpdateProductByAdmin;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Presentation.Controllers.Common;
using ShelfApi.Presentation.Controllers.ProductController.Inputs;

namespace ShelfApi.Presentation.Controllers.ProductController;

[Route("admin/products")]
public class AdminProductController(ISender sender) : AdminBaseController(sender)
{
    [ProducesResponseType(typeof(Result<ProductUserView>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<Result<ProductUserView>>> AddProductByAdminAsync([FromBody] AddProductByAdminInput inputBody)
    {
        Result<ProductUserView> result = await _sender.Send(new AddProductByAdminCommand
        {
            Name = inputBody.Name,
            Price = inputBody.Price,
            Quantity = inputBody.Quantity
        });

        return result;
    }

    [ProducesResponseType(typeof(Result<ProductUserView>), StatusCodes.Status200OK)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Result<ProductUserView>>> UpdateProductByAdminAsync(
        [FromRoute] long id,
        [FromBody] UpdateProductByAdminInput inputBody)
    {
        Result<ProductUserView> result = await _sender.Send(new UpdateProductByAdminCommand
        {
            Id = id,
            Name = inputBody.Name,
            Price = inputBody.Price,
            Quantity = inputBody.Quantity
        });

        return result;
    }

    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<bool>>> DeleteProductByAdminAsync([FromRoute] long id)
    {
        Result<bool> result = await _sender.Send(new DeleteProductByAdminCommand
        {
            Id = id
        });

        return result;
    }
}