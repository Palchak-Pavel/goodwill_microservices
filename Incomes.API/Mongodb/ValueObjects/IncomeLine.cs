using Incomes.API.Common;

namespace Incomes.API.Mongodb.ValueObjects;

public class IncomeLine : ValueObject
{
	public IncomeLine(int incomeQuantity, string productCode, decimal priceFob)
	{
		IncomeQuantity = incomeQuantity;
		ProductCode = productCode;
		PriceFob = priceFob;
	}
	public int IncomeQuantity { get; private set; }
	public string ProductCode { get; private set; }
	public decimal PriceFob { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return IncomeQuantity;
		yield return ProductCode;
		yield return PriceFob;
	}

	public decimal IncomeSum => IncomeQuantity * PriceFob;
	
}
