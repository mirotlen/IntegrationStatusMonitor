namespace IntegrationStatusMonitor.Reports;

internal class OrderedErrorsCountReport : IReport
{
    public string Name => "Posortowana według liczby lista blędów (od największej)";

    public IReadOnlyList<string> GetReport(IReadOnlyList<IntegrationLog> logs)
    {
        var sortedErrors = logs
            .Where(l => !l.IsSuccess)
            .GroupBy(l => l.CustomerName)
            .OrderByDescending(g => g.Count())
            .ToDictionary(l => l.Key, v => v.ToList());

        var report = new List<string>();
        foreach (var item in sortedErrors)
        {
            var errors = String.Join(", ", item.Value.Select(f => f.ErrorMessage).Count());
            report.Add($"Klient - {item.Key}:  Ilość błędów: {errors}");
        }
        return report;
    }
}