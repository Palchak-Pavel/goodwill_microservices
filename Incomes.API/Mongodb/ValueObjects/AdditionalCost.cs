using Incomes.API.Common;

namespace Incomes.API.Mongodb.ValueObjects;

public class IncomeAdditionalCost : ValueObject
{
    public IncomeAdditionalCost(string? name, double? price)
    {
        Name = name;
        Price = price;
    }
    public string? Name { get; private set; }
    public double? Price { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Price;
    }
}