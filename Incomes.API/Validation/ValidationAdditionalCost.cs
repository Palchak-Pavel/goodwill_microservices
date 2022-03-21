﻿using FluentValidation;

namespace Incomes.API.Validation;

public class ValidationAdditionalCost : AbstractValidator<Mongodb.Entities.AdditionalCost>
{
    public ValidationAdditionalCost()
    {
        RuleFor(additionalCost => additionalCost.Id)
            .NotNull()
            .NotEmpty();
        RuleFor(additionalCost => additionalCost.Name)
            .NotNull()
            .NotEmpty();
    }
}