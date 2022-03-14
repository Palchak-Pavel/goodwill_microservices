using Incomes.API.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Incomes.API.Mongodb.Entities;


public class Supplier : EntityBase
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string? CountryName { get; set; }
    public string? CurrencyType { get; set; }
    public string? SupplierName { get; set; }
    public double? Margin { get; set; }
    public double? DeliveryDays { get; set; }
}
