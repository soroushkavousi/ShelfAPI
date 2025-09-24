using System.Reflection;

namespace Company.Shared.Common;

public static class AssemblyHelper
{
    /// <summary>
    ///     Lazily cached list of all project assemblies.
    ///     Initialized once on first call to <see cref="GetAllProjectAssemblies" />.
    ///     Thread-safe thanks to Lazy.
    /// </summary>
    private static readonly Lazy<List<Assembly>> _cachedAssemblies = new(FindAssemblies);

    /// <summary>
    ///     Returns all assemblies that belong to the solution’s root namespace.
    ///     Uses cached result after the first call.
    /// </summary>
    public static List<Assembly> GetAllProjectAssemblies() => _cachedAssemblies.Value;

    /// <summary>
    ///     Actual scanning/assembly loading logic. Executed only once.
    /// </summary>
    private static List<Assembly> FindAssemblies()
    {
        string callingAssemblyName = Assembly.GetEntryAssembly()?.GetName().Name
            ?? throw new InvalidOperationException("Cannot determine calling assembly name.");
        string rootNamespace = callingAssemblyName.Split('.')[0];

        string baseDirectory = AppContext.BaseDirectory;

        string[] dllFiles = Directory.GetFiles(baseDirectory, "*.dll", SearchOption.TopDirectoryOnly);

        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (string dllPath in dllFiles)
        {
            string assemblyName = Path.GetFileNameWithoutExtension(dllPath);

            // Skip assemblies that don’t match the root namespace convention.
            if (!assemblyName.StartsWith(rootNamespace + ".", StringComparison.OrdinalIgnoreCase) &&
                !assemblyName.Equals(rootNamespace, StringComparison.OrdinalIgnoreCase))
                continue;

            // Skip if assembly is already loaded in memory.
            if (loadedAssemblies.Any(a =>
                a.GetName().Name?.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) == true))
                continue;

            try
            {
                Assembly.Load(assemblyName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load assembly {assemblyName}: {ex.Message}");
            }
        }

        // Return all assemblies in AppDomain matching the root namespace pattern.
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name is { } name &&
                (name.StartsWith(rootNamespace + ".", StringComparison.OrdinalIgnoreCase) ||
                    name.Equals(rootNamespace, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }
}