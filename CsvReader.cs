namespace IntegrationStatusMonitor;
public interface ICsvReader
{
    IReadOnlyList<IntegrationLog> Read(string path, char separator = ',');
}

internal class CsvReader : ICsvReader
{
    public IReadOnlyList<IntegrationLog> Read(string path, char separator = ',')
    {
        var content = ReadFile(path);
        if (content.Length == 0)
        {
            return [];
        }
        var csvHeaders = content.First().Split(separator);
        var rows = content.Skip(1).ToArray();

        var logs = new List<IntegrationLog>();

        foreach (var row in rows)
        {
            var cells = row.Split(separator);

            if (cells.Length < csvHeaders.Length)
            {
                throw new InvalidOperationException("Liczba nagłówków nie odpowiada liczbie kolumn w wierszu.");
            }

            logs.Add(new IntegrationLog(
                DateTime.Parse(cells[0]),
                cells[1],
                cells[2],
                cells[3],
                cells[4],
                String.IsNullOrEmpty(cells[5]) ? null : cells[5]
            ));
        }

        return logs;
    }
    private string[] ReadFile(string file)
    {
        try
        {
            if (!File.Exists(file))
            {
                return [];
            }
            var result = File.ReadAllLines(file);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania pliku: {ex.Message}");
            return [];
        }
    }
}