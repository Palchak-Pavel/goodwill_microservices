namespace EventBus.Messages.Events;

public class IncomeCreateEvent
{
    public string Id { get; set; } = null!;
    public string? Сomment { get; set; }
    public DateTime? СonfirmedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CurrencyType { get; set; } = null!;
    public decimal FactSum { get; set; } 
    public string IncomeName { get; set; } = null!;
    public string IncomeState { get; set; } = null!;
    public decimal InvoiceSum { get; set; }
    public decimal SeaSum { get; set; }
    public string SupplierName { get; set; } = null!;
}