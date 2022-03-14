using IncomePayments.API.Common;

namespace IncomePayments.API.Mongodb.ValueObjects;

public class Penalty : ValueObject
{
    public Penalty(string? comment, string? name, double? penaltySum)
    {
        Comment = comment;
        Name = name;
        PenaltySum = penaltySum;
    }

    public string? Comment { get; private set; }
    public string? Name { get; private set; }
    public double? PenaltySum { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Comment;
        yield return Name;
        yield return PenaltySum;
    }
}