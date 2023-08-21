using FluentValidation;
using System.Globalization;

namespace TaxCalculate.Application.Requests
{
    public class CalculateValuesRequestValidator : AbstractValidator<CalculateValuesRequest>
    {
        private static readonly string MORE_THAN_ONE_VALUE_ERROR = "More than one value input is not allowed";
        private static readonly string VALUE_MISSING_OR_INVALID_ERROR = "{0} value is missing or invalid (0 or non-numeric)";
        private static readonly int[] VALID_AUSTRIAN_TAX_RATE = new [] { 10, 13, 20 };

        public CalculateValuesRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => x.NetValue is null && x.VatValue is null)
                .DependentRules(() =>
                {
                    RuleFor(x => x.GrossValue)
                        .Must(value => !string.IsNullOrEmpty(value) && decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue) && parsedValue > 0)
                        .WithMessage(string.Format(VALUE_MISSING_OR_INVALID_ERROR, "Gross"));
                })
                .When(x => x.GrossValue is not null)
                .WithMessage(MORE_THAN_ONE_VALUE_ERROR);

            RuleFor(x => x)
               .Must(x => x.GrossValue is null && x.VatValue is null)
               .DependentRules(() =>
               {
                   RuleFor(x => x.NetValue)
                       .Must(value => !string.IsNullOrEmpty(value) && decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue) && parsedValue > 0)
                       .WithMessage(string.Format(VALUE_MISSING_OR_INVALID_ERROR, "Net"));
               })
               .When(x => x.NetValue is not null)
               .WithMessage(MORE_THAN_ONE_VALUE_ERROR);

            RuleFor(x => x)
               .Must(x => x.GrossValue is null && x.NetValue is null)
               .DependentRules(() =>
               {
                   RuleFor(x => x.VatValue)
                       .Must(value => !string.IsNullOrEmpty(value) && decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue) && parsedValue > 0)
                       .WithMessage(string.Format(VALUE_MISSING_OR_INVALID_ERROR, "Vat"));
               })
               .When(x => x.VatValue is not null)
               .WithMessage(MORE_THAN_ONE_VALUE_ERROR);

            RuleFor(x => x.VatRate)
                .Must(value => !string.IsNullOrEmpty(value) && int.TryParse(value, out var parsedValue) && VALID_AUSTRIAN_TAX_RATE.Contains(parsedValue))
                .WithMessage("Vat rate is missing or invalid");
        }
    }
}
