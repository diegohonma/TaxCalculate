using TaxCalculate.Services;
using TaxCalculate.Services.DTOs;

namespace TaxCalculate.Tests.TaxCalculate.Services
{
    internal class CalculateFromGrossValueServiceTests
    {

        private CalculateFromGrossValueService _calculateFromGrossValueService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _calculateFromGrossValueService = new CalculateFromGrossValueService();
        }

        [Test]
        public void Should_Return_True_When_CalculateFromGrossValue()
        {
            Assert.That(
                _calculateFromGrossValueService.IsCalculateFrom(new CalculateValuesDto(12.00M, null, null, 20)), Is.True);
        }

        [Test]
        public void Should_Return_False_When_CalculateFromAnotherValue()
        {
            Assert.That(
                _calculateFromGrossValueService.IsCalculateFrom(new CalculateValuesDto(null, 12.00M, null, 20)), Is.False);
        }

        [TestCase(20, 12.00, 10.00, 2.00)]
        [TestCase(13, 19.21, 17.00, 2.21)]
        public void Should_Return_CalculatedLiquidAndGrossValue(int vatRate, decimal grossValue, decimal expectedNetValue, decimal expectedVatValue)
        {
            var calculatedValues = _calculateFromGrossValueService.Calculate(new CalculateValuesDto(grossValue, null, null, vatRate));
            
            Assert.Multiple(() =>
            {
                Assert.That(calculatedValues.NetValue, Is.EqualTo(expectedNetValue));
                Assert.That(calculatedValues.VatValue, Is.EqualTo(expectedVatValue));
            });
        }
    }
}
