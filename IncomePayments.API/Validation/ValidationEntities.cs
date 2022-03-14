using FluentValidation;
using IncomePayments.API.Mongodb.Entities;
using IncomePayments.API.Mongodb.ValueObjects;

namespace IncomePayments.API.Validation;

public class ValidationIncomePayment : AbstractValidator<IncomePayment>
{
    public ValidationIncomePayment()
    {
        RuleFor(payment => payment.Id)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.Сomment)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.СonfirmedAt)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.CreatedAt)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.CurrencyType)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.FactSum)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.IncomeName)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.IncomeState)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.InvoiceSum)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.SeaSum)
            .NotNull()
            .NotEmpty();
        RuleFor(payment => payment.SupplierName)
            .NotNull()
            .NotEmpty();

        RuleForEach(payment => payment.Payments).SetValidator(new ValidationPayment());
        RuleForEach(payment => payment.Penalties).SetValidator(new ValidationPenalty());
    }

    public class ValidationPayment : AbstractValidator<Payment>
    {
        public ValidationPayment()
        {
            RuleFor(payment => payment.Comment)
                .NotNull()
                .NotEmpty();
            RuleFor(payment => payment.CreatedAt)
                .NotNull()
                .NotEmpty();
            RuleFor(payment => payment.PaymentType)
                .NotNull()
                .NotEmpty();
            RuleFor(payment => payment.PaymentSum)
                .NotNull()
                .NotEmpty();
        }
    }

    public class ValidationPenalty : AbstractValidator<Penalty>
    {
        public ValidationPenalty()
        {
            RuleFor(penalty => penalty.Comment)
                .NotNull()
                .NotEmpty();
            RuleFor(penalty => penalty.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(penalty => penalty.PenaltySum)
                .NotNull()
                .NotEmpty();
        }
    }
}