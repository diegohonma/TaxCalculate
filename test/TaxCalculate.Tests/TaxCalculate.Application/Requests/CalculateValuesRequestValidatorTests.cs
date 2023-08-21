using TaxCalculate.Application.Requests;

namespace TaxCalculate.Tests.TaxCalculate.Application.Requests
{
    internal class CalculateValuesRequestValidatorTests
    {
        [TestCase("aaaa")]
        [TestCase("0")]
        public void Should_Return_InvalidGrossValue(string grossValue)
        {
            var validator = new CalculateValuesRequestValidator();

            var result = validator.Validate(new CalculateValuesRequest(grossValue, null, null, "20"));

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Any(e => e.ErrorMessage == "Gross value is missing or invalid (0 or non-numeric)"), Is.True);
            });
        }

        [TestCase("aaaa")]
        [TestCase("0")]
        public void Should_Return_InvalidNetValue(string netValue)
        {
            var validator = new CalculateValuesRequestValidator();

            var result = validator.Validate(new CalculateValuesRequest(null, netValue, null, "20"));

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Any(e => e.ErrorMessage == "Net value is missing or invalid (0 or non-numeric)"), Is.True);
            });
        }

        [TestCase("aaaa")]
        [TestCase("0")]
        public void Should_Return_InvalidVatValue(string vatvalue)
        {
            var validator = new CalculateValuesRequestValidator();

            var result = validator.Validate(new CalculateValuesRequest(null, null, vatvalue, "20"));

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Any(e => e.ErrorMessage == "Vat value is missing or invalid (0 or non-numeric)"), Is.True);
            });
        }

        [Test]
        public void Should_Return_MoreThanOneInputNotAllowed()
        {
            var validator = new CalculateValuesRequestValidator();

            var resultWithoutGrossValue = validator.Validate(new CalculateValuesRequest(null, "10", "2", "20"));
            var resultWithoutNetValue = validator.Validate(new CalculateValuesRequest("12", null, "2", "20"));
            var resultWithoutVatValue = validator.Validate(new CalculateValuesRequest("12", "10", null, "20"));

            Assert.Multiple(() =>
            {
                Assert.That(resultWithoutGrossValue.IsValid, Is.False);
                Assert.That(resultWithoutGrossValue.Errors.Any(e => e.ErrorMessage == "More than one value input is not allowed"), Is.True);

                Assert.That(resultWithoutNetValue.IsValid, Is.False);
                Assert.That(resultWithoutNetValue.Errors.Any(e => e.ErrorMessage == "More than one value input is not allowed"), Is.True);

                Assert.That(resultWithoutVatValue.IsValid, Is.False);
                Assert.That(resultWithoutVatValue.Errors.Any(e => e.ErrorMessage == "More than one value input is not allowed"), Is.True);
            });
        }

        [TestCase("aaaa")]
        [TestCase("0")]
        [TestCase("11")]
        public void Should_Return_InvalidVatRate(string vatRate)
        {
            var validator = new CalculateValuesRequestValidator();

            var result = validator.Validate(new CalculateValuesRequest(null, null, null, vatRate));

            Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Any(e => e.ErrorMessage == "Vat rate is missing or invalid"), Is.True);
            });
        }

        [Test]
        public void Should_Return_Success()
        {
            var validator = new CalculateValuesRequestValidator();

            var resultWithGrossValue = validator.Validate(new CalculateValuesRequest("12", null, null, "20"));
            var resultWithNetValue = validator.Validate(new CalculateValuesRequest(null, "10", null, "20"));
            var resultWithTaxValue = validator.Validate(new CalculateValuesRequest(null, null, "2", "20"));

            Assert.Multiple(() =>
            {
                Assert.That(resultWithGrossValue.IsValid, Is.True);
                Assert.That(resultWithNetValue.IsValid, Is.True);
                Assert.That(resultWithTaxValue.IsValid, Is.True);
            });
        }
    }
}
