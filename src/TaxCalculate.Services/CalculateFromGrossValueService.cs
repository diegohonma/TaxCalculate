using TaxCalculate.Services.DTOs;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Services
{
    public class CalculateFromGrossValueService : ICalculateService
    {
        public bool IsCalculateFrom(CalculateValuesDto calculateValues) =>
            calculateValues is not null
                && calculateValues.GrossValue is not null
                && calculateValues.NetValue is null
                && calculateValues.VatValue is null;

        public CalculateValuesDto Calculate(CalculateValuesDto calculateValues)
        {
            var netValue = calculateValues.GrossValue / (1 + (calculateValues.VatRate / 100M));
            var vatValue = calculateValues.GrossValue - netValue;

            return new CalculateValuesDto(
              calculateValues.GrossValue,
              netValue,
              vatValue,
              calculateValues.VatRate);
        }
    }
}
