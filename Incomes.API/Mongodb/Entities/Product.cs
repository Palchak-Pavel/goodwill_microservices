using Incomes.API.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Incomes.API.Mongodb.Entities;

public class Product : EntityBase
{
	
	[BsonId(IdGenerator = typeof(StringObjectIdGenerator))] 
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }
	
	public  string? ProductCode { get; set; }
	
}
