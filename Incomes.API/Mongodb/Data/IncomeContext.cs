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

            Incomes = database.GetCollection<Income>("Incomes");
            AdditionalCosts = database.GetCollection<AdditionalCost>("AdditionalCosts");
            Products = database.GetCollection<Product>("Products");
            Suppliers = database.GetCollection<Supplier>("Suppliers");
        }
        public IMongoCollection<Income> Incomes { get; }

        public IMongoCollection<AdditionalCost> AdditionalCosts { get; }

        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<Supplier> Suppliers { get; }

    
    }
}
