using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;

namespace Incomes.API.Dto
{
    public class IncomeDto 
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? СonfirmedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        public string? CurrencyType { get; set; }
        public string IncomeName { get; set; } = null!;
        public string IncomeState { get; set; }
        public string SupplierName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal IncomeSum { get; set; }
    }
}
