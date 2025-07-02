using IntegrationStatusMonitor.Statistics;
using System.Globalization;

namespace IntegrationStatusMonitor;

internal partial class Program
{
    static void Main(string[] args)
    {
        var pathProvider = new CsvPathsProvider();
        var filesPaths = pathProvider.GetPaths();

        var selectedPath = ChooseFileFrom(filesPaths);

        var dateRange = GetDatesFromUser();

        var csvReader = new CsvReader();
        var cells = csvReader.Read(selectedPath);

        var filteredCells = Filter(cells, dateRange);

        var statisticsProvider = new StatisticsProvider();
        var statistics = statisticsProvider.GetStatistics();

        Console.WriteLine("\n---------------- Statystyki podsumowujące ----------------");

        foreach (var statistic in statistics)
        {
            Console.WriteLine($"\n{statistic.Name}: {statistic.GetStatistic(filteredCells)}");
            Console.WriteLine("\n---------------------------------------------------------");
        }

        Console.WriteLine("\n                          ***                            \n");
        Console.WriteLine("\n---------------- Raport statusu klientów ----------------");

        var reportsProvider = new Reports.ReportsProvider();
        var reports = reportsProvider.GetReports();
        
        foreach (var report in reports)
        {
            Console.WriteLine($"\n{report.Name} \n");
            var reportContent = report.GetReport(filteredCells);
            foreach (var line in reportContent)
            {
                Console.WriteLine($"* {line}");
            }
            Console.WriteLine("\n---------------------------------------------------------");
        }
        
    }
    private static string ChooseFileFrom(string[] filesPaths)
    {
        Console.WriteLine("\nZnalezione pliki CSV:");
        for (int i = 0; i < filesPaths.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileName(filesPaths[i])}");
        }

        Console.Write("\nWybierz numer pliku do wczytania: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > filesPaths.Length)
        {
            Console.WriteLine("Nieprawidłowy wybór.");
            return ChooseFileFrom(filesPaths);
        }

        string selectedFile = filesPaths[choice - 1];
        Console.WriteLine($"\nWczytywano plik: {Path.GetFileName(selectedFile)}\n");
        return selectedFile;
    }

    private static DateRange GetDatesFromUser()
    {
        Console.WriteLine("Podaj zakres dat w formacie dd.MM.yyyy lub naciśnij Enter, aby pominąć.");
        Console.Write("Data początkowa: ");
        var isDateStart = TryGetDate(out var startDate);
        Console.Write("Data końcowa: ");
        var isDateEnd = TryGetDate(out var endDate);
        return new DateRange(
            isDateStart ? startDate : DateTime.MinValue,
            isDateEnd ? endDate : DateTime.MaxValue);
    }
    private static bool TryGetDate(out DateTime date)
    {
        if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            return false;
        }
        return true;
    }

    static IReadOnlyList<IntegrationLog> Filter(IReadOnlyList<IntegrationLog> data, DateRange dateRange)
    {
        var result = data.Where(log => log.Timestamp.Date >= dateRange.StartDate && log.Timestamp.Date <= dateRange.EndDate)
            .ToList();
        return result;
    }
}