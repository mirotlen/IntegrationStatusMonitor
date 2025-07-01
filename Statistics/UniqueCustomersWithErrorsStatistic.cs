namespace IntegrationStatusMonitor.Statistics;

internal class UniqueCustomersWithErrorsStatistic : IStatistics
{
    public string Name => "Liczba unikalnych klientów z błędami";

    public string GetStatistic(IReadOnlyList<IntegrationLog> log)
    {
        var uniqueCustomersWithErrors = log
            .Where(l => !l.IsSuccess)
            .Select(l => l.CustomerId)
            .Distinct()
            .Count();

        return $"{uniqueCustomersWithErrors}";
    }
}