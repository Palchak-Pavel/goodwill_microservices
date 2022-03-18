using Incomes.API.Mongodb.Entities;
using MongoDB.Driver;

namespace Incomes.API.Mongodb.Data;

public interface IMongoIncomeContext
{
    IMongoCollection<AdditionalCost> AdditionalCosts { get; }
    IMongoCollection<Income> Incomes { get; }
    IMongoCollection<Product> Products { get; }
    IMongoCollection<Supplier> Suppliers { get; }

}
