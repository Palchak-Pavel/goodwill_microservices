using IncomePayments.API.Mongodb.Entities;
using MongoDB.Bson.Serialization;

namespace IncomePayments.API.Mongodb;

public class EntitiesConfig
{
    public static void Config()
    {
        BsonClassMap.RegisterClassMap<IncomePayment>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
        });
    }
}