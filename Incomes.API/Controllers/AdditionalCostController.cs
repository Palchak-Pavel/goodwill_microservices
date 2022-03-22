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
        var additionalCosts = await _context.AdditionalCosts.Find(x => true).ToListAsync();
        return Ok(additionalCosts);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Income), (int)HttpStatusCode.OK)]

    public async Task<bool> UpdateAdditionalCost([FromBody] AdditionalCost additionalCosts)
    {
        var updateIncome = await _context.AdditionalCosts.
            ReplaceOneAsync(filter: g => g.Id == additionalCosts.Id, replacement: additionalCosts);

        return updateIncome.IsAcknowledged && updateIncome.ModifiedCount > 0;
    }


    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<AdditionalCost>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<AdditionalCost>>> CreateAdditionalCost([FromBody]AdditionalCost additionalCosts)
    {
        await _context.AdditionalCosts.InsertOneAsync(additionalCosts);
        var createAdditionalCost = new 
        {
            Id = additionalCosts.Id,
            Name = additionalCosts.Name 
        };
        return Ok(createAdditionalCost);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AdditionalCost), (int)HttpStatusCode.OK)]
    public async Task<bool> DeleteAdditionalCost(string id)
    {
        FilterDefinition<AdditionalCost> filter = Builders<AdditionalCost>.Filter.Eq(p => p.Id, id);

        var deleteResult = await _context
                                 .AdditionalCosts
                                 .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }


}


