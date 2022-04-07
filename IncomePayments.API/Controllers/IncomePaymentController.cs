using System.Net;
using IncomePayments.API.Mongodb.Data;
using IncomePayments.API.Mongodb.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace IncomePayments.API.Controllers;

[ApiController]
[Route("income_payments")]
public class IncomePaymentController : ControllerBase
{
    private IMongoIncomePaymentContext _context;

    public IncomePaymentController(IMongoIncomePaymentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IncomePayment>), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> GetIncomePayment(string name, int year)
    {
        var startDate = new DateTime(year, 1, 1);
        var endDate = new DateTime(year + 1, 1, 1);

        FilterDefinition<IncomePayment> filter = Builders<IncomePayment>.Filter.Eq(p => p.SupplierName, name) &
                                                 Builders<IncomePayment>.Filter.Gte(p => p.CreatedAt, startDate) &
                                                 Builders<IncomePayment>.Filter.Lt(p => p.CreatedAt, endDate);

        var payment = await _context
            .IncomePayment
            .Find(filter)
            .ToListAsync();
        ;

        return Ok(payment);

    }

    [HttpPut]
    [ProducesResponseType(typeof(IncomePayment), (int) HttpStatusCode.OK)]

    public async Task<bool> UpdateIncomePayment([FromBody] IncomePayment incomePayment)
    {
        var updatePayment =
            await _context.IncomePayment.ReplaceOneAsync(filter: g => g.Id == incomePayment.Id,
                replacement: incomePayment);

        return updatePayment.IsAcknowledged && updatePayment.ModifiedCount > 0;
    }
}