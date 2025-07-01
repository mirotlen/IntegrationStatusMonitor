namespace IntegrationStatusMonitor.Reports;

public interface IReportsProvider
{
    IReadOnlyList<IReport> GetReports();
}
internal class ReportsProvider : IReportsProvider
{
    public IReadOnlyList<IReport> GetReports()
    {
        return
        [
            new FailedIntegrationCliensReport(),
            new NewestErrorWithTotalErrorCountReport(),
            new OrderedErrorsCountReport()
        ];
    }
}
