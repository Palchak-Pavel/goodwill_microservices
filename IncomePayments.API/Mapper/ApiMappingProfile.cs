using AutoMapper;
using EventBus.Messages.Events;
using IncomePayments.API.Mongodb.Entities;

namespace IncomePayments.API.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<IncomeCreateEvent, IncomePayment>();
    }
}

