using Incomes.API.Mongodb.Entities;
using MongoDB.Driver;

namespace Incomes.API.Mongodb.Data;

public interface IMongoIncomeContext
{
    IMongoCollection<AdditionalCost> AdditionalCost { get; }
    IMongoCollection<Income> Income { get; }
    IMongoCollection<Product> Product { get; }
    IMongoCollection<Supplier> Supplier { get; }

}
