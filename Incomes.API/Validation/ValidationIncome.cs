using FluentValidation;
using Incomes.API.Mongodb.Entities;
using Incomes.API.Mongodb.ValueObjects;

namespace Incomes.API.Validation;

public class ValidationIncome: AbstractValidator<Income>
{
    public ValidationIncome()
    {
        RuleFor(income => income.Id)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.СonfirmedAt)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.CreatedAt)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.CurrencyType)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.IncomeName)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.IncomeState)
            .NotNull()
            .NotEmpty();
        RuleFor(income => income.SupplierName)
            .NotNull()
            .NotEmpty();

        RuleForEach(income => income.AdditionalCosts).SetValidator(new ValidationAdditionalCost());
        RuleForEach(income => income.IncomeLines).SetValidator(new ValidationIncomeLine());
    }

    public class ValidationAdditionalCost : AbstractValidator<Incomes.API.Mongodb.ValueObjects.AdditionalCost>
    {
        public ValidationAdditionalCost()
        {
            RuleFor(income => income.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(income => income.Price)
                .NotNull()
                .NotEmpty();
        }
    }
    public class ValidationIncomeLine : AbstractValidator<IncomeLine>
    {
        public ValidationIncomeLine()
        {
            RuleFor(income => income.IncomeQuantity)
                .NotNull()
                .NotEmpty();
            RuleFor(income => income.ProductCode)
                .NotNull()
                .NotEmpty();
            RuleFor(income => income.PriceFob)
                .NotNull()
                .NotEmpty();
        }
    }
}