using Incomes.API.Mongodb.Entities;
using MongoDB.Bson.Serialization;

namespace Incomes.API.Common;

public class EntitiesConfig
{
	public static void Config()
	{
        BsonClassMap.RegisterClassMap<Mongodb.Entities.Income>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
        });

     /*   BsonClassMap.RegisterClassMap<AdditionalCost>(x =>
        {
            x.AutoMap();
            x.SetIgnoreExtraElements(true);
        });*/
    }
}
