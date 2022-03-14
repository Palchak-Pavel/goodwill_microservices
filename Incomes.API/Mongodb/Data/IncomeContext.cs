using Incomes.API.Mongodb.Entities;
using MongoDB.Driver;

namespace Incomes.API.Mongodb.Data
{
    public class IncomeContext : IMongoIncomeContext
    {
        public IncomeContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Income = database.GetCollection<Income>("Incomes");
            AdditionalCost = database.GetCollection<AdditionalCost>("AdditionalCosts");
            Product = database.GetCollection<Product>("Products");
            Supplier = database.GetCollection<Supplier>("Suppliers");
        }
        public IMongoCollection<Income> Income { get; }

        public IMongoCollection<AdditionalCost> AdditionalCost { get; }

        public IMongoCollection<Product> Product { get; }

        public IMongoCollection<Supplier> Supplier { get; }

    
    }
}
