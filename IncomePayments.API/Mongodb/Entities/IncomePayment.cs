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
    public string? Id { get; set; }

    public string? Сomment { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? СonfirmedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CreatedAt { get; set; }
    public string? CurrencyType { get; set; }
    public double? FactSum { get; set; }
    public string? IncomeName { get; set; }
    public string? IncomeState { get; set; }
    public double? InvoiceSum { get; set; }
    public double? SeaSum { get; set; }

    [BsonElement("SupplierName")]
    public string? SupplierName { get; set; }
    public Payment[]? Payments { get; set; }
    public Penalty[]? Penalties { get; set; }
}
