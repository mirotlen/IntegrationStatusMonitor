namespace IntegrationStatusMonitor;
public class IntegrationLog
{
    public DateTime Timestamp { get; }

    public string CustomerId { get; }
    public string CustomerName { get; }
    public string IntegrationType { get; }
    public string Status { get; }
    public bool IsSuccess => Status == "Success";
    public string? ErrorMessage { get; }

    public IntegrationLog(
        DateTime timestamp, 
        string customerId, 
        string customerName, 
        string integrationType, 
        string status, 
        string? errorMessage)
    {
        Timestamp = timestamp;
        CustomerId = customerId;
        CustomerName = customerName;
        IntegrationType = integrationType;
        Status = status;
        ErrorMessage = errorMessage;
    }
}
