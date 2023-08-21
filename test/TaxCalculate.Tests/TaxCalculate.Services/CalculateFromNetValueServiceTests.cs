using TaxCalculate.Services;
using TaxCalculate.Services.DTOs;

namespace TaxCalculate.Tests.TaxCalculate.Services
{
    internal class CalculateFromNetValueServiceTests
    {

        private CalculateFromNetValueService _calculateFromNetValueService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _calculateFromNetValueService = new CalculateFromNetValueService();
        }

        [Test]
        public void Should_Return_True_When_CalculateFromNetValue()
        {
            Assert.That(
                _calculateFromNetValueService.IsCalculateFrom(new CalculateValuesDto(null, 10.00M, null, 20)), Is.True);
        }

        [Test]
        public void Should_Return_False_When_CalculateFromAnotherValue()
        {
            Assert.That(
                _calculateFromNetValueService.IsCalculateFrom(new CalculateValuesDto(10.00M, null, null, 20)), Is.False);
        }

        [TestCase(20, 10.00, 12.00, 2.00)]
        [TestCase(13, 17.00, 19.21, 2.21)]
        public void Should_Return_CalculatedLiquidAndGrossValue(int vatRate, decimal netValue, decimal expectedGrossValue, decimal expectedVatValue)
        {
            var calculatedValues = _calculateFromNetValueService.Calculate(new CalculateValuesDto(null, netValue, null, vatRate));
            
            Assert.Multiple(() =>
            {
                Assert.That(calculatedValues.GrossValue, Is.EqualTo(expectedGrossValue));
                Assert.That(calculatedValues.VatValue, Is.EqualTo(expectedVatValue));
            });
        }
    }
}
