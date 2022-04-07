using System.ComponentModel.DataAnnotations;

namespace EventBus.Entities;

public class IncomeEventBus 
{
    public string Id { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime?  СonfirmedAt { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; }
    
    public string? CurrencyType { get; set; }
    public string IncomeName { get; set; } = null!;
    public string IncomeState { get; set; }

    public string SupplierName { get; set; }
    public string? Gtd { get; set; }
    
    public double? Margin { get; set; }
    
   // public IncomeLine[]? IncomeLines { get; set; }
    //public ValueObjects.AdditionalCost[]? AdditionalCosts { get; set; }
}
