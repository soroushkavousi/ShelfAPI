using IdGen;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShelfApi.Shared.Common.Interfaces;
using IdGeneratorOptions = ShelfApi.Application.Common.Services.IdGeneratorOptions;

namespace ShelfApi.Infrastructure.Tools;

public class IdGenerator : IIdGenerator
{
    private readonly IdGen.IdGenerator _generator;

    public IdGenerator(IOptions<IdGeneratorOptions> options)
    {
        IdGeneratorOptions optionsValue = options.Value;
        int generatorId = optionsValue.GeneratorId;
        DateTime epoch = optionsValue.EpochStart;

        if (generatorId is < 0 or > 1023)
            throw new ArgumentOutOfRangeException(nameof(generatorId), "GeneratorId must be between 0 and 1023");

        IdStructure idStructure = new(42, 10, 11); // 42 bits timestamp, 10 bits generator, 11 bits sequence

        DefaultTimeSource timeSource = new(epoch);

        IdGen.IdGeneratorOptions idGeneratorOptions = new(idStructure, timeSource);
        _generator = new(generatorId, idGeneratorOptions);
    }

    public long GenerateId() => _generator.CreateId();
}

public static class IdGeneratorExtensions
{
    public static void AddIdGenerator(this IServiceCollection services,
        Action<IdGeneratorOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddSingleton<IIdGenerator, IdGenerator>();
    }
}