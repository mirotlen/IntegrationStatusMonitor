namespace IntegrationStatusMonitor;

public interface ICsvPathsProvider
{
    string[] GetPaths();
}
internal class CsvPathsProvider : ICsvPathsProvider
{
    private const string defaulRelativePathtoTestCsv = "TestData";
    private const string searchPattern = "*.csv";

    public string[] GetPaths()
    {
        var filesExist = TryGetPaths(defaulRelativePathtoTestCsv, out var csvFilesPaths);

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
            csvFilesPaths = Directory.GetFiles(defaulRelativePathtoTestCsv, searchPattern);
            if (csvFilesPaths.Length > 0)
            {
                return true;
            }
        }
        return false;
    }
}
