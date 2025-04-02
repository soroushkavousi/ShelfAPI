namespace ShelfApi.Domain.Common.Model;

public abstract class BaseModel
{
    protected BaseModel()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; protected init; }
    public DateTime? ModifiedAt { get; protected set; }

    public void SetModifiedAt(DateTime modifiedAt)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(modifiedAt, CreatedAt);

        ModifiedAt = modifiedAt;
    }
}