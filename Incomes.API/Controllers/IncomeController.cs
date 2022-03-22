using Incomes.API.Dto;
using Incomes.API.Mongodb.Data;
using Incomes.API.Mongodb.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Incomes.API.Controllers;

[ApiController]
[Route("incomes")]

public class IncomeController : ControllerBase
{
    private IMongoIncomeContext _context;

    public IncomeController(IMongoIncomeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes()
    {
        var incomes = await _context.Incomes.Find(x => true).ToListAsync();


        //// Созадли список
        //List<IncomeDto> incomesDtosList = new List<IncomeDto>();

        //// Запускаем цикл по сущностям
        //foreach(var i in incomes)
        //{
        //    // для каждой сущности делаем DTO
        //    var incomeDto = new IncomeDto
        //    {
        //        Id = i.Id,
        //        CreatedAt = i.CreatedAt,
        //        // i.IncomeLines?.Sum - если IncomeLines не NULL, то суммировать IncomeQuantity
        //        //  ?? 0 - если NULL, то сумма = 0
        //        ProductQuantity = i.IncomeLines?.Sum(x => x.IncomeQuantity) ?? 0,
        //        IncomeSum = i.IncomeLines?.Sum(x => x.IncomeSum) ?? 0
        //    };
        //    incomesDtosList.Add(incomeDto);
        //}

        var incomesDtos = from i in incomes
                          select new IncomeDto
                          {
                              Id = i.Id,
                              CreatedAt = i.CreatedAt,
                              СonfirmedAt = i.СonfirmedAt,
                              CurrencyType = i.CurrencyType,
                              IncomeName = i.IncomeName,
                              IncomeState = i.IncomeState,
                              SupplierName = i.SupplierName,
                              // i.IncomeLines?.Sum - если IncomeLines не NULL, то суммировать IncomeQuantity
                              //  ?? 0 - если NULL, то сумма = 0
                              ProductQuantity = i.IncomeLines?.Sum(x => x.IncomeQuantity) ?? 0,
                              IncomeSum = i.IncomeLines?.Sum(x => x.IncomeSum) ?? 0

                          };

        return Ok(incomesDtos);
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
            income.AdditionalCosts = additionalCosts.Select(x => new Mongodb.ValueObjects.AdditionalCost(x.Id, 0)).ToArray();
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
        income.AdditionalCosts = Array.Empty<Mongodb.ValueObjects.AdditionalCost>();

        await _context.Incomes.InsertOneAsync(income);
        var result = new IncomeDto
        {
            Id = income.Id,
            СonfirmedAt = income.СonfirmedAt,
            CreatedAt = income.CreatedAt,
            CurrencyType = income.CurrencyType,
            IncomeName = income.IncomeName,
            IncomeState = income.IncomeState,
            SupplierName = income.SupplierName,
            ProductQuantity = income.IncomeLines?.Sum(x => x.IncomeQuantity) ?? 0,
            IncomeSum = income.IncomeLines?.Sum(x => x.IncomeSum) ?? 0
        };
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