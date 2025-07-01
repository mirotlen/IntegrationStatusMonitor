namespace IntegrationStatusMonitor.Reports;

internal class FailedIntegrationCliensReport : IReport
{
    public string Name => "Lista klientów z niedziałającymi integracjami";

    public IReadOnlyList<string> GetReport(IReadOnlyList<IntegrationLog> logs)
    {
        var uniqueCustomersWithErrors = logs
            .Where(l => !l.IsSuccess)
            .GroupBy(l => l.CustomerName)
            .ToDictionary(l => l.Key, v => v.ToList());

        var report = new List<string>();
        foreach (var item in uniqueCustomersWithErrors)
        {
            var failedIntegrations = String.Join(", ", item.Value.Select(f => f.IntegrationType));
            report.Add($"Klient - {item.Key};  Niedziałające integracje: {failedIntegrations}");
        }

        return report;
    }
}
