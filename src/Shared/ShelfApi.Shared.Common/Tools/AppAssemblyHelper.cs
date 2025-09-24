using System.Reflection;
using Company.Shared.Common;

namespace ShelfApi.Shared.Common.Tools;

public class AppAssemblyHelper
{
    public static Assembly[] GetAllProjectAssemblies()
    {
        List<Assembly> assemblies = AssemblyHelper.GetAllProjectAssemblies();
        return assemblies.ToArray();
    }
}