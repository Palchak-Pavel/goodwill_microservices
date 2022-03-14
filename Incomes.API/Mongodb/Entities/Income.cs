using System.ComponentModel.DataAnnotations;
using Incomes.API.Common;
using Incomes.API.Mongodb.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Incomes.API.Mongodb.Entities;

[BsonIgnoreExtraElements]
public class Income : EntityBase
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))] 
    [BsonRepresentation(BsonType.ObjectId)]
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

    //[DisplayFormat(ConvertEmptyStringToNull = false)]
    public string SupplierName { get; set; }
    public string? Gtd { get; set; }
    
    public double? Margin { get; set; }
    
    public IncomeLine[]? IncomeLines { get; set; }
    public Incomes.API.Mongodb.ValueObjects.AdditionalCost[]? AdditionalCosts { get; set; }
}
