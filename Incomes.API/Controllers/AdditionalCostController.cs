using Incomes.API.Mongodb.Data;
using Incomes.API.Mongodb.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace Incomes.API.Controllers;

[ApiController]
[Route("additional_costs")]

public class AdditionalCostController : ControllerBase
{
    private IMongoIncomeContext _context;

    public AdditionalCostController(IMongoIncomeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AdditionalCost>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<AdditionalCost>>> GetAdditionalCosts()
    {
        var additionalCosts = await _context.AdditionalCost.Find(x => true).ToListAsync();
        return Ok(additionalCosts);
    }

    
  
}


