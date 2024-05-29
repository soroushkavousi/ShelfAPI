namespace ShelfApi.Domain.Common;

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

public abstract class BaseModel<TKey> : BaseModel
{
    protected BaseModel()
    { }

    public BaseModel(TKey id) : base()
    {
        Id = id;
    }

    public TKey Id { get; }
}