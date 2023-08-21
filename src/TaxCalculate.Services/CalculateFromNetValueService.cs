using TaxCalculate.Services.DTOs;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Services
{
    public class CalculateFromNetValueService : ICalculateService
    {
        public bool IsCalculateFrom(CalculateValuesDto calculateValues) => 
            calculateValues is not null 
                && calculateValues.NetValue is not null
                && calculateValues.GrossValue is null
                && calculateValues.VatValue is null;

        public CalculateValuesDto Calculate(CalculateValuesDto calculateValues)
        {
            var vatValue = (calculateValues.NetValue * calculateValues.VatRate) / 100;
            var grossValue = calculateValues.NetValue + vatValue;

            return new CalculateValuesDto(
                grossValue,
                calculateValues.NetValue,
                vatValue,
                calculateValues.VatRate);
        }
    }
}
