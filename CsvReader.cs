namespace IntegrationStatusMonitor;
public interface ICsvReader
{
    IReadOnlyList<IntegrationLog> ReadAs(string[] content, char separator = ',');
}

internal class CsvReader : ICsvReader
{
    public IReadOnlyList<IntegrationLog> ReadAs(string[] content, char separator = ',')
    {
        var csvHeaders = content.First().Split(separator);
        var rows = content.Skip(1).ToArray();

        var logs = new List<IntegrationLog>();

        foreach (var row in rows)
        {
            var cells = row.Split(separator);

            if (cells.Length < csvHeaders.Length)
            {
                throw new ArgumentException(); // edit nspisać text do exeptiona
            }

            logs.Add(new IntegrationLog(
                DateTime.Parse(cells[0]),
                cells[1],
                cells[2],
                cells[3],
                cells[4],
                String.IsNullOrEmpty(cells[5]) ? cells[5] : null
            ));
        }

        return logs;
    }
}