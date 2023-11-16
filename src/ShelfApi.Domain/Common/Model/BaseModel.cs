namespace ShelfApi.Domain.Common;

public abstract class BaseModel<TKey>
{
    protected BaseModel() { }

    public BaseModel(TKey id) : this()
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    public TKey Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    public void SetModifiedAt(DateTime modifiedAt)
    {
        if (modifiedAt <= CreatedAt)
            throw new ArgumentOutOfRangeException(nameof(modifiedAt));

        ModifiedAt = modifiedAt;
    }
}