namespace ShelfApi.Domain.Common.Model;

public abstract class BaseModel
{
    public BaseModel()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    public void SetModifiedAt(DateTime modifiedAt)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(modifiedAt, CreatedAt);

        ModifiedAt = modifiedAt;
    }
}