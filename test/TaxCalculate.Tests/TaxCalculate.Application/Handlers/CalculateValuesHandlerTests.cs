using FluentValidation;
using Moq;
using TaxCalculate.Application.Exceptions;
using TaxCalculate.Application.Handlers;
using TaxCalculate.Application.Requests;
using TaxCalculate.Services.DTOs;
using TaxCalculate.Services.Interfaces;

namespace TaxCalculate.Tests.TaxCalculate.Application.Handlers
{
    internal class CalculateValuesHandlerTests
    {
        private CalculateValuesHandler _handler;
        private AbstractValidator<CalculateValuesRequest> _validator = new CalculateValuesRequestValidator();
        private Mock<ICalculateService> _calculateService;

        [SetUp]
        public void SetUp()
        {
            _calculateService = new Mock<ICalculateService>();
            _handler = new CalculateValuesHandler(
                new List<ICalculateService>()
                { 
                    _calculateService.Object
                },
                _validator);
        }

        [Test]
        public void Should_Return_CalculatedValues()
        {
            var expectedResponse = new CalculateValuesDto(12.00M, 10.00M, 2.00M, 20);

            _calculateService
                .Setup(c => c.IsCalculateFrom(It.IsAny<CalculateValuesDto>()))
                .Returns(true);

            _calculateService
                .Setup(c => c.Calculate(It.IsAny<CalculateValuesDto>()))
                .Returns(expectedResponse);

            var response = _handler.Handle(new CalculateValuesRequest(null, null, "2", "20"));

            Assert.Multiple(() =>
            {
                Assert.That(response.GrossValue, Is.EqualTo(expectedResponse.GrossValue));
                Assert.That(response.NetValue, Is.EqualTo(expectedResponse.NetValue));
                Assert.That(response.VatValue, Is.EqualTo(expectedResponse.VatValue));
                Assert.That(response.VatRate, Is.EqualTo(expectedResponse.VatRate));

                _calculateService
                    .Verify(c => c.IsCalculateFrom(It.IsAny<CalculateValuesDto>()), Times.Once);

                _calculateService
                    .Verify(c => c.Calculate(It.IsAny<CalculateValuesDto>()), Times.Once);
            });
        }

        [Test]
        public void Should_Throw_ValidationException_When_RequestNotValid()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<BusinessValidationException>(() => _handler.Handle(new CalculateValuesRequest("12", "10", "2", "20")));

                _calculateService
                    .Verify(c => c.IsCalculateFrom(It.IsAny<CalculateValuesDto>()), Times.Never);

                _calculateService
                    .Verify(c => c.Calculate(It.IsAny<CalculateValuesDto>()), Times.Never);
            });
        }
    }
}
