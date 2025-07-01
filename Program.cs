using IntegrationStatusMonitor.Statistics;

namespace IntegrationStatusMonitor;

internal class Program
{
    static void Main(string[] args)
    {
        var userInterface = new UserInterface();
        var content = userInterface.Run();

        var csvReader = new CsvReader();
        var cells = csvReader.ReadAs(content);

        var statisticsProvider = new StatisticsProvider();
        var statistics = statisticsProvider.GetStatistics();

        Console.WriteLine("-----------------Statystyki podsumowujące-----------------");

        foreach (var statistic in statistics)
        {
            Console.WriteLine($"\n{statistic.Name}: {statistic.GetStatistic(cells)}");
            Console.WriteLine("\n---------------------------------------------------------");
        }

        Console.WriteLine("\n                          ***                            \n");
        Console.WriteLine("\n-----------------Raport statusu klientów-----------------");

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


        /*foreach (var cell in cells)
        //{
        //    Console.WriteLine(
        //        $"{cell.Timestamp} " +
        //        $"{cell.CustomerId} " +
        //        $"{cell.CustomerName} " +
        //        $"{cell.IntegrationType} " +
        //        $"{cell.Status} " +
        //        $"{cell.ErrorMessage}"
        //    );
        //}
        */
    }
}