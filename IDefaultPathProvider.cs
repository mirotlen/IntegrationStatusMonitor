namespace IntegrationStatusMonitor;

internal interface IDefaultPathProvider
{
    string GetPath();
}
internal class DefaultPathProvider : IDefaultPathProvider
{
    private const string _defaulRelativePathToTestCsv = "TestData";
    public string GetPath()
    {
        return _defaulRelativePathToTestCsv;
    }
}
