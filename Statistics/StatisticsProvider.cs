namespace IntegrationStatusMonitor.Statistics;

public interface IStatisticsProvider
{
    IStatistics[] GetStatistics();
}

internal class StatisticsProvider : IStatisticsProvider
{
    public IStatistics[] GetStatistics()
    {
        return
        [
            new TotalAttemptsStatistic(),
            new SuccessRateStatistic(),
            new UniqueCustomersWithErrorsStatistic()
        ];
    }
}
