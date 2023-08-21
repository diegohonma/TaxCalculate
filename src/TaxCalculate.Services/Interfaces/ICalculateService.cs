using TaxCalculate.Services.DTOs;

namespace TaxCalculate.Services.Interfaces
{
    public interface ICalculateService
    {
        bool IsCalculateFrom(CalculateValuesDto calculateValues);

        CalculateValuesDto Calculate(CalculateValuesDto calculateValues);
    }
}
