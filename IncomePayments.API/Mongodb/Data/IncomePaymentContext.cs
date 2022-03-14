using IncomePayments.API.Mongodb.Entities;
using MongoDB.Driver;

namespace IncomePayments.API.Mongodb.Data;

public class IncomePaymentContext : IMongoIncomePaymentContext
{

    public IncomePaymentContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        IncomePayment = database.GetCollection<IncomePayment>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
    }
    public IMongoCollection<IncomePayment> IncomePayment { get; }

}
