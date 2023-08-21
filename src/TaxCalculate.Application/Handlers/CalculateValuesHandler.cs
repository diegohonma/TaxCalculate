using FluentValidation;
using System.Globalization;
using TaxCalculate.Application.Exceptions;
using TaxCalculate.Application.Handlers.Interfaces;
using TaxCalculate.Application.Requests;
using TaxCalculate.Application.Responses;
using TaxCalculate.Services.DTOs;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Application.Handlers
{
    public class CalculateValuesHandler : ICalculateValuesHandler
    {
        private readonly IReadOnlyCollection<ICalculateService> _calculateServices;
        private readonly AbstractValidator<CalculateValuesRequest> _validator;

        public CalculateValuesHandler(
            IReadOnlyCollection<ICalculateService> calculateServices,
            AbstractValidator<CalculateValuesRequest> validator)
        {
            _calculateServices = calculateServices;
            _validator = validator;
        }

        public CalculateValuesResponse Handle(CalculateValuesRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                throw new BusinessValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage);

            var dto = MapToDto(request);
            var calculatedValues = _calculateServices.First(c => c.IsCalculateFrom(dto)).Calculate(dto);

            return MapToResponse(calculatedValues);
        }

        private static CalculateValuesDto MapToDto(CalculateValuesRequest request) => new(
            string.IsNullOrEmpty(request.GrossValue) ? null : decimal.Parse(request.GrossValue, NumberStyles.Any, CultureInfo.InvariantCulture),
            string.IsNullOrEmpty(request.NetValue) ? null : decimal.Parse(request.NetValue, NumberStyles.Any, CultureInfo.InvariantCulture),
            string.IsNullOrEmpty(request.VatValue) ? null : decimal.Parse(request.VatValue, NumberStyles.Any, CultureInfo.InvariantCulture),
            int.Parse(request.VatRate!));

        private static CalculateValuesResponse MapToResponse(CalculateValuesDto dto) => new(
            dto.GrossValue!.Value,
            dto.NetValue!.Value,
            dto.VatValue!.Value, 
            dto.VatRate);
    }
}
