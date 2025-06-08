namespace ShelfApi.Application.Common.Services;

public record IdGeneratorOptions
{
    public int GeneratorId { get; set; }
    public DateTime EpochStart { get; set; }
}