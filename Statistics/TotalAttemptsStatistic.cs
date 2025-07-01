namespace IntegrationStatusMonitor.Statistics;

internal class TotalAttemptsStatistic : IStatistics
{
    public string Name => "Łączna liczba prób wykonania integracji";

    public string GetStatistic(IReadOnlyList<IntegrationLog> log)
    {
        return $"{log.Count}";
    }
}
