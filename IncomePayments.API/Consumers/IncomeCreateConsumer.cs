using AutoMapper;
using EventBus.Messages.Events;
using IncomePayments.API.Mongodb.Data;
using IncomePayments.API.Mongodb.Entities;
using MassTransit;

namespace IncomePayments.API.Consumers;

public class IncomeCreateConsumer: IConsumer<IncomeCreateEvent>
{
    private readonly IMongoIncomePaymentContext _dbContext;
    private readonly IMapper _mapper;

    public IncomeCreateConsumer(IMongoIncomePaymentContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task Consume(ConsumeContext<IncomeCreateEvent> context)
    {
        
        var eventMessage = context.Message;
        var incomePayment = _mapper.Map<IncomePayment>(eventMessage);
        
        
         await _dbContext.IncomePayment.InsertOneAsync(incomePayment);
    }
}