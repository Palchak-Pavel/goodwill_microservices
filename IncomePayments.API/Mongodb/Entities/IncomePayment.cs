using System.ComponentModel.DataAnnotations;
using IncomePayments.API.Common;
using IncomePayments.API.Mongodb.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace IncomePayments.API.Mongodb.Entities;

[BsonIgnoreExtraElements]
public class IncomePayment : EntityBase
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string? Сomment { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? СonfirmedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedAt { get; set; }
    public string CurrencyType { get; set; } = null!;
    public decimal FactSum { get; set; }
    public string IncomeName { get; set; } = null!;
    public string IncomeState { get; set; } = null!;
    public decimal InvoiceSum { get; set; }
    public decimal SeaSum { get; set; }

    [BsonElement("SupplierName")]
    public string SupplierName { get; set; } = null!;
    public Payment[]? Payments { get; set; }
    public Penalty[]? Penalties { get; set; }
}
