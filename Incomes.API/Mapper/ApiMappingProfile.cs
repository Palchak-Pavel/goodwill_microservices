using AutoMapper;
using EventBus.Messages.Events;
using Incomes.API.Dto;
using Incomes.API.Mongodb.Entities;

namespace Incomes.API.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<Income, IncomeDto>()
            .ForMember(x => x.ProductQuantity, 
                expr => 
                    expr.MapFrom(z => z.IncomeLines.Sum(y => y.IncomeQuantity)))
            
            .ForMember(x => x.IncomeSum, 
                expr => 
                    expr.MapFrom(z => z.IncomeLines.Sum(y => y.IncomeQuantity * y.PriceFob)));

        CreateMap<Income, IncomeCreateEvent>()
            .ForMember(x => x.InvoiceSum, 
                expr => 
                    expr.MapFrom(z => z.IncomeLines.Sum(y => y.IncomeQuantity* y.PriceFob)))
            
            .ForMember(x => x.FactSum, 
                expr => 
                    expr.MapFrom(z => z.IncomeLines.Sum(y => y.IncomeQuantity * y.PriceFob)));
    }
}

