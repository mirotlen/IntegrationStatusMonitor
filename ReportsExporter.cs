namespace IntegrationStatusMonitor;
public interface IReportsExporter
{
    Task<string> ExportAsync(IReadOnlyList<string> data);
}
internal class ReportsExporter : IReportsExporter
{
    private readonly IDefaultPathProvider _defaultPathProvider;
    private const string fileExtension = ".txt";
    private const string fileName = "ExportedResults";
    public ReportsExporter(IDefaultPathProvider defaultPathProvider)
    {
        _defaultPathProvider = defaultPathProvider;
    }
    public async Task<string> ExportAsync(IReadOnlyList<string> data)
    {
        var basePath = _defaultPathProvider.GetPath();
        var fullPath = Path.Combine(basePath, fileName + fileExtension);
        await File.WriteAllLinesAsync(fullPath, data);
        return fullPath;
    }
}