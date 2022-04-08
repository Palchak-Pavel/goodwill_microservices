using System.ComponentModel.DataAnnotations;
using Incomes.API.Mongodb.ValueObjects;

namespace Incomes.API.Dto
{
    public class CreateIncomeDto
    {
        public string? Gtd { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? СonfirmedAt { get; set; }

        public double? Margin { get; set; } = 1;
        public string IncomeState { get; set; } = "В дороге";

        public IncomeAdditionalCost[]? AdditionalCosts { get; set; }

    }
}
