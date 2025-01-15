namespace ShelfApi.Presentation.Tools;

public class Utils
{
    public static string ContentRootPath
    {
        get
        {
            var rootDirectory = AppContext.BaseDirectory;
            if (rootDirectory.Contains("bin"))
            {
                rootDirectory = rootDirectory[..rootDirectory.IndexOf("bin")];
            }

            return rootDirectory;
        }
    }
}