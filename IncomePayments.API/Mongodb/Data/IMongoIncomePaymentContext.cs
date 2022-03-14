using IncomePayments.API.Mongodb.Entities;
using MongoDB.Driver;

namespace IncomePayments.API.Mongodb.Data
{
    public interface IMongoIncomePaymentContext
    {
        IMongoCollection<IncomePayment> IncomePayment { get; }
    }
}