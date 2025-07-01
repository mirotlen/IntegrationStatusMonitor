namespace IntegrationStatusMonitor.Statistics;

internal class SuccessRateStatistic : IStatistics
{
    public string Name => "Procent sukcesu";

    public string GetStatistic(IReadOnlyList<IntegrationLog> log)
    {
        if (log.Count == 0) return "Brak danych.";

        var successCount = log.Count(l => l.IsSuccess);
        var successRate = (double)successCount / log.Count * 100;

        return $"{successRate:F2}%";
    }
}
