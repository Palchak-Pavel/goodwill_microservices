using EventBus.Messages.Events;
using IncomePayments.API.Mongodb.Data;
using IncomePayments.API.Mongodb.Entities;
using MassTransit;

namespace IncomePayments.API.Consumers;

public class IncomeCreateConsumer: IConsumer<IncomeCreateEvent>
{
    private readonly IMongoIncomePaymentContext _dbContext;

    public IncomeCreateConsumer(IMongoIncomePaymentContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }


    public async Task Consume(ConsumeContext<IncomeCreateEvent> context)
    {
        
        var eventMessage = context.Message;
         var incomePayment = new IncomePayment
         {
             Id = eventMessage.Id,
             СonfirmedAt = eventMessage.СonfirmedAt,
             CreatedAt = eventMessage.CreatedAt,
             CurrencyType = eventMessage.CurrencyType,
             IncomeName = eventMessage.IncomeName,
             IncomeState = eventMessage.IncomeState,
             SupplierName = eventMessage.SupplierName,
             InvoiceSum = eventMessage.InvoiceSum,
             FactSum = eventMessage.FactSum,
             SeaSum = eventMessage.SeaSum
         };
        
         await _dbContext.IncomePayment.InsertOneAsync(incomePayment);
    }
}