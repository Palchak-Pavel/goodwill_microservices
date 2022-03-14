using Incomes.API.Dto;
using Incomes.API.Mongodb.Data;
using Incomes.API.Mongodb.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace Incomes.API.Controllers;

[ApiController]
[Route("incomes")]

 public class IncomeController : ControllerBase
 {
    private  IMongoIncomeContext _context;

    public IncomeController(IMongoIncomeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes()
    {
        var incomes = await _context.Income.Find(x => true).ToListAsync();
 

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

  
    [HttpGet ("{id}")]
    [ProducesResponseType(typeof(IEnumerable<Income>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Income>> GetId(string id)
    {
        var income = await _context.Income.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (income == null) return NotFound();

        if(income.IncomeState == "В дороге")
        {
            var additionalCosts = await _context.AdditionalCost.Find(x => true).ToListAsync();
            income.AdditionalCosts = additionalCosts.Select(x => new Mongodb.ValueObjects.AdditionalCost(x.Id, 0)).ToArray();
        }
        return income;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CreateIncomeDto>>> CreateIncome([FromBody] CreateIncomeDto incomesDto)
    {
        Income item = new()
        {
            Gtd = incomesDto.Gtd,
            СonfirmedAt = incomesDto.СonfirmedAt,
            Margin = incomesDto.Margin,
            IncomeState = incomesDto.IncomeState,
            AdditionalCosts = incomesDto.AdditionalCosts,
                                
        };
         return CreatedAtAction(nameof(CreateIncome), item);
    }


    [HttpPut]
    [ProducesResponseType(typeof(Income), (int)HttpStatusCode.OK)]

    public async Task<bool> UpdateAdditionalCost([FromBody] Income additionalCost)
    {
        var updateAdditionalCost = await _context.Income.
            ReplaceOneAsync(filter: g => g.Id == additionalCost.Id, replacement: additionalCost);

        return updateAdditionalCost.IsAcknowledged && updateAdditionalCost.ModifiedCount > 0;
    }
}