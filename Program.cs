using IntegrationStatusMonitor.Statistics;

namespace IntegrationStatusMonitor;

internal class Program
{
    static void Main(string[] args)
    {
        var pathProvider = new CsvPathsProvider();
        var filesPaths = pathProvider.GetPaths();

        var selectedPath = ChooseFileFrom(filesPaths);

        var csvReader = new CsvReader();
        var cells = csvReader.Read(selectedPath);

        var statisticsProvider = new StatisticsProvider();
        var statistics = statisticsProvider.GetStatistics();

        Console.WriteLine("\n---------------- Statystyki podsumowujące ----------------");

        foreach (var statistic in statistics)
        {
            Console.WriteLine($"\n{statistic.Name}: {statistic.GetStatistic(cells)}");
            Console.WriteLine("\n---------------------------------------------------------");
        }

        Console.WriteLine("\n                          ***                            \n");
        Console.WriteLine("\n---------------- Raport statusu klientów ----------------");

        var reportsProvider = new Reports.ReportsProvider();
        var reports = reportsProvider.GetReports();
        
        foreach (var report in reports)
        {
            Console.WriteLine($"\n{report.Name} \n");
            var reportContent = report.GetReport(cells);
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

        Console.WriteLine("\nWybierz numer pliku do wczytania:");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > filesPaths.Length)
        {
            Console.WriteLine("Nieprawidłowy wybór.");
            return ChooseFileFrom(filesPaths);
        }

        string selectedFile = filesPaths[choice - 1];
        Console.WriteLine($"\nWczytywanie pliku: {Path.GetFileName(selectedFile)}\n");
        return selectedFile;
    }
}