using Incomes.API.Dto;
using Incomes.API.Mongodb.Data;
using Incomes.API.Mongodb.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using EventBus.Messages.Events;
using Incomes.API.Mapper;
using Incomes.API.Mongodb.ValueObjects;
using MassTransit;

namespace Incomes.API.Controllers;

[ApiController]
[Route("incomes")]

public class IncomeController : ControllerBase
{
    private readonly IMongoIncomeContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public IncomeController(IMongoIncomeContext context, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes()
    {
        var incomes = await _context.Incomes.Find(x => true).ToListAsync();


        var mapIncome = _mapper.Map<IList<IncomeDto>>(incomes);
        
        return Ok(mapIncome);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Income>> GetId(string id)
    {
        var income = await _context.Incomes.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (income == null) return NotFound();

        if (income.IncomeState == "В дороге")
        {
            var additionalCosts = await _context.Incomes.Find(x => true).ToListAsync();
            income.AdditionalCosts = additionalCosts.Select(x => new IncomeAdditionalCost(x.Id, 0)).ToArray();
        }
        return income;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> CreateIncome([FromBody] Income income)
    {
        income.Gtd = null;
        income.СonfirmedAt = null;
        income.Margin = 1;
        income.IncomeState = "В дороге";
        income.AdditionalCosts = Array.Empty<IncomeAdditionalCost>();

        await _context.Incomes.InsertOneAsync(income);

        
        var eventMessage = _mapper.Map<IncomeCreateEvent>(income);
        /*var eventMessage = new IncomeCreateEvent
        {
            Id = income.Id,
            СonfirmedAt = income.СonfirmedAt,
            CreatedAt = income.CreatedAt,
            CurrencyType = income.CurrencyType,
            IncomeName = income.IncomeName,
            IncomeState = income.IncomeState,
            SupplierName = income.SupplierName,
            InvoiceSum =  income.IncomeLines?.Sum(x => x.IncomeQuantity * x.PriceFob) ?? 0,
            FactSum = income.IncomeLines?.Sum(x => x.IncomeQuantity * x.PriceFob) ?? 0,
        };*/
        
        await _publishEndpoint.Publish(eventMessage);

        var result = _mapper.Map<IncomeDto>(income);
       
        return Ok(result);
    }


    [HttpPut]
    [ProducesResponseType(typeof(Income), (int)HttpStatusCode.OK)]

    public async Task<bool> UpdateIncome([FromBody] Income income)
    {
        var updateIncome = await _context.Incomes.
            ReplaceOneAsync(filter: g => g.Id == income.Id, replacement: income);

        return updateIncome.IsAcknowledged && updateIncome.ModifiedCount > 0;
    }


    public class CompositeObject
    {
        public string Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ConfirmedAt { get; set; }
    }

    [HttpPut("change_date")]
    [ProducesResponseType(typeof(CompositeObject), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateСonfirmedAt([FromBody] CompositeObject changeDate)
    {
        var filter = Builders<Income>.Filter.Eq(x => x.Id, changeDate.Id);
        var income = await _context.Incomes.Find(filter).FirstOrDefaultAsync();
        if (income == null) return NotFound();

        income.СonfirmedAt = changeDate.ConfirmedAt;
        var updateСonfirmedAt = await _context.Incomes.FindOneAndReplaceAsync(filter, income, null);

        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Income), (int)HttpStatusCode.OK)]
    public async Task<bool> DeleteIncome(string id)
    {
        FilterDefinition<Income> filter = Builders<Income>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
                                            .Incomes
                                            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
}