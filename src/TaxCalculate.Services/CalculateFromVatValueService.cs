using TaxCalculate.Services.DTOs;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Services
{
    public class CalculateFromVatValueService : ICalculateService
    {
        public bool IsCalculateFrom(CalculateValuesDto calculateValues) =>
            calculateValues is not null
                && calculateValues.VatValue is not null
                && calculateValues.GrossValue is null
                && calculateValues.NetValue is null;

        public CalculateValuesDto Calculate(CalculateValuesDto calculateValues)
        {
            var netValue = (calculateValues.VatValue * 100) / calculateValues.VatRate;
            var grossValue = netValue + calculateValues.VatValue;

            return new CalculateValuesDto(
              grossValue,
              netValue,
              calculateValues.VatValue,
              calculateValues.VatRate);
        }
    }
}
