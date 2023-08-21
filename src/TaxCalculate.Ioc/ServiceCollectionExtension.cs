using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculate.Application.Handlers;
using TaxCalculate.Application.Handlers.Interfaces;
using TaxCalculate.Application.Requests;
using TaxCalculate.Services;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Ioc
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            RegisterApplicationHandlers(services);
            RegisterValidations(services);
            RegisterServices(services);
        }

        private static void RegisterApplicationHandlers(IServiceCollection services)
        {
            services.AddScoped<ICalculateValuesHandler, CalculateValuesHandler>();
        }

        private static void RegisterValidations(IServiceCollection services)
        {
            services.AddScoped<AbstractValidator<CalculateValuesRequest>, CalculateValuesRequestValidator>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IReadOnlyCollection<ICalculateService>>(_ =>
            {
                return new List<ICalculateService>()
                {
                    new CalculateFromGrossValueService(),
                    new CalculateFromNetValueService(),
                    new CalculateFromVatValueService()
                };
            });
        }
    }
}
