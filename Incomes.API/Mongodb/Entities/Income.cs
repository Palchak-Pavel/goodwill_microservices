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
    public string Id { get; set; } = null!;
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime?  СonfirmedAt { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; }
    public string CurrencyType { get; set; } = null!;
    public string IncomeName { get; set; } = null!;
    public string IncomeState { get; set; } = null!;
    public string SupplierName { get; set; } = null!;
    public string? Gtd { get; set; }
    public double Margin { get; set; }
    
    public IncomeLine[]? IncomeLines { get; set; }
    public IncomeAdditionalCost[]? AdditionalCosts { get; set; }
}
