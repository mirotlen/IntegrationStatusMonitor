namespace IntegrationStatusMonitor;

internal partial class Program
{
    public class DateRange
    {
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}