namespace IntegrationStatusMonitor;

public interface ICsvFilesPathsProvider
{
    string[] GetPaths();
}
internal class CsvFilesPathsProvider : ICsvFilesPathsProvider
{
    private readonly IDefaultPathProvider _defaultPathProvider;
    private const string searchPattern = "*.csv";

    public CsvFilesPathsProvider(IDefaultPathProvider defaultPathProvider)
    {
        _defaultPathProvider = defaultPathProvider;
    }

    public string[] GetPaths()
    {
        var filesExist = TryGetPaths(_defaultPathProvider.GetPath(), out var csvFilesPaths);

        while (!filesExist)
        {
            Console.WriteLine("Podaj ścieżkę do folderu z plikami .csv:");
            var folderPath = Console.ReadLine();
            filesExist = TryGetPaths(folderPath, out csvFilesPaths, searchPattern);
        }
        return csvFilesPaths;
    }
    private bool TryGetPaths(string folderPath, out string[] csvFilesPaths, string searchPattern = "*.csv")
    {
        csvFilesPaths = [];
        var folderExists = Directory.Exists(folderPath);

        if (folderExists)
        {
            csvFilesPaths = Directory.GetFiles(_defaultPathProvider.GetPath(), searchPattern);
            if (csvFilesPaths.Length > 0)
            {
                return true;
            }
        }
        return false;
    }
}
