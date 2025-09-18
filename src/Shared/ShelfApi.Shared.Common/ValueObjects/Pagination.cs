using System.Text.Json.Serialization;

namespace ShelfApi.Shared.Common.ValueObjects;

public record Pagination
{
    public Pagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        From = (PageNumber - 1) * PageSize;
    }

    public Pagination(int pageNumber, int pageSize, int totalItems) : this(pageNumber, pageSize)
    {
        SetTotalItems(totalItems);
    }

    [JsonIgnore]
    public int From { get; private set; }

    public int PageNumber { get; }
    public int PageSize { get; }
    public int? TotalItems { get; private set; }
    public int? TotalPages { get; private set; }

    public void SetTotalItems(int totalItems)
    {
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(TotalItems.Value / (double)PageSize);
    }
}