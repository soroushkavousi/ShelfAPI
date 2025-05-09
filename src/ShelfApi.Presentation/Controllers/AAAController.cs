// using MassTransit;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using ShelfApi.Domain.Common.Interfaces;
// using ShelfApi.Domain.ProductAggregate.Events;
// using ShelfApi.Infrastructure.Data.ShelfApiDb;
// using ShelfApi.Presentation.Tools;
//
// namespace ShelfApi.Presentation.Controllers;
//
// [ApiController]
// [Produces(Constants.JsonContentTypeName)]
// public class AAAController(ILogger<AAAController> logger,
//     ISender sender, IPublishEndpoint publishEndpoint, ShelfApiDbContext shelfApiDbContext) : ControllerBase
// {
//     [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
//     [HttpPost("Test1")]
//     public async Task<ActionResult<Result<string>>> Test1Async()
//     {
//         ProductCreatedDomainEvent productCreatedDomainEvent = new()
//         {
//             Product = new ProductEventDto
//             {
//                 Id = 1,
//                 Name = "Product AH112",
//                 Price = 100,
//                 Quantity = 10,
//                 IsElasticsearchSynced = false,
//                 CreatedAt = DateTime.Now,
//                 IsDeleted = false
//             }
//         };
//         IDomainEvent domainEvent = productCreatedDomainEvent;
//         // await publishEndpoint.Publish(domainEvent);
//         await publishEndpoint.Publish(domainEvent, domainEvent.GetType());
//         
//         Console.WriteLine($"{shelfApiDbContext.ChangeTracker.DebugView.ShortView}");
//
//         await shelfApiDbContext.SaveChangesAsync();
//         return Ok("done");
//     }
//
//     [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
//     [HttpPost("Test2")]
//     public async Task<ActionResult<Result<string>>> Test2Async()
//     {
//         ProductCreatedDomainEvent productCreatedDomainEvent = new() { Product = new ProductEventDto { Id = 1, Name = "test" } };
//         await publishEndpoint.Publish(productCreatedDomainEvent);
//         return Ok("done");
//     }
// }

