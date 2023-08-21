using TaxCalculate.Application.Requests;
using TaxCalculate.Application.Responses;

namespace TaxCalculate.Application.Handlers.Interfaces
{
    public interface ICalculateValuesHandler
    {
        CalculateValuesResponse Handle(CalculateValuesRequest request);
    }
}
