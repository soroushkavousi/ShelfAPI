namespace ShelfApi.Application.ProductApplication.Commands.DeleteProductByAdmin;

public class DeleteProductByAdminCommand : IRequest<Result<bool>>
{
    public required long Id { get; init; }
}