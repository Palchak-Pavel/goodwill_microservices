using IncomePayments.API.Common;

namespace IncomePayments.API.Mongodb.ValueObjects;

public class Payment : ValueObject
{
    public Payment(string? comment, DateTime? createdAt, string? paymentType, double? paymentSum)
    {
        Comment = comment;
        CreatedAt = createdAt;
        PaymentType = paymentType;
        PaymentSum = paymentSum;
    }

    public string? Comment { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public string? PaymentType { get; private set; }
    public double? PaymentSum { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Comment;
        yield return CreatedAt;
        yield return PaymentType;
        yield return PaymentSum;
    }
}