using TaxCalculate.Services;
using TaxCalculate.Services.DTOs;

namespace TaxCalculate.Tests.TaxCalculate.Services
{
    internal class CalculateFromVatValueServiceTests
    {

        private CalculateFromVatValueService _calculateFromVatValueService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _calculateFromVatValueService = new CalculateFromVatValueService();
        }

        [Test]
        public void Should_Return_True_When_CalculateFromVatValue()
        {
            Assert.That(
                _calculateFromVatValueService.IsCalculateFrom(new CalculateValuesDto(null, null, 2.00M, 20)), Is.True);
        }

        [Test]
        public void Should_Return_False_When_CalculateFromAnotherValue()
        {
            Assert.That(
                _calculateFromVatValueService.IsCalculateFrom(new CalculateValuesDto(1, null, null, 20)), Is.False);
        }

        [TestCase(20, 2.00, 10.00, 12.00)]
        [TestCase(13, 2.21, 17.00, 19.21)]
        public void Should_Return_CalculatedLiquidAndGrossValue(int vatRate, decimal vatValue, decimal expectedNetValue, decimal expectedGross)
        {
            var calculatedValues = _calculateFromVatValueService.Calculate(new CalculateValuesDto(null, null, vatValue, vatRate));
            
            Assert.Multiple(() =>
            {
                Assert.That(calculatedValues.NetValue, Is.EqualTo(expectedNetValue));
                Assert.That(calculatedValues.GrossValue, Is.EqualTo(expectedGross));
            });
        }
    }
}
