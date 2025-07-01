namespace IntegrationStatusMonitor.Statistics;

public interface IStatistics
{
    string Name { get; }
    string GetStatistic(IReadOnlyList<IntegrationLog> log);
}
