namespace ShelfApi.Domain.Common;

public class BaseModel
{
    protected BaseModel() { }

    public BaseModel(ulong id)
        : this()
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    public ulong Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    public void SetModifiedAt(DateTime modifiedAt)
    {
        if (modifiedAt <= CreatedAt)
            throw new ArgumentOutOfRangeException(nameof(modifiedAt));

        ModifiedAt = modifiedAt;
    }
}
