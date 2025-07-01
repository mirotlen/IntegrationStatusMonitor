namespace IntegrationStatusMonitor.Reports;

public interface IReport
{
    string Name { get; }
    IReadOnlyList<string> GetReport(IReadOnlyList<IntegrationLog> logs);
}
