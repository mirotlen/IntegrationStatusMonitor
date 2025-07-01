namespace IntegrationStatusMonitor.Reports;

internal class NewestErrorWithTotalErrorCountReport : IReport
{
    public string Name => "Liczba błędów i najnowszy błąd";

    public IReadOnlyList<string> GetReport(IReadOnlyList<IntegrationLog> logs)
    {
        var errorCount = logs.Count(l => !l.IsSuccess);
        var newestError = logs
            .Where(l => !l.IsSuccess)
            .OrderByDescending(l => l.Timestamp)
            .FirstOrDefault();

        var report = new List<string>
        {
            $"Łączna liczba błędów: {errorCount}",
            newestError != null
                ? $"Najnowszy błąd: {newestError.ErrorMessage};  Klient: {newestError.CustomerName};  Integracja: {newestError.IntegrationType};  Czas: {newestError.Timestamp}"
                : "Najnowszy błąd: brak"
        };
        return report;
    }
}
