using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TaxCalculate.Api.Controllers;
using TaxCalculate.Application.Handlers.Interfaces;
using TaxCalculate.Application.Requests;
using TaxCalculate.Application.Responses;

namespace TaxCalculate.Tests.TaxCalculate.Api.Controllers
{
    internal class PricesControllerTests
    {
        private PricesController _pricesController;
        private Mock<ICalculateValuesHandler> _handler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _handler = new Mock<ICalculateValuesHandler>();
            _pricesController = new PricesController(_handler.Object);
        }

        [Test]
        public void Should_Return_Ok()
        {
            var expectedResponse = new CalculateValuesResponse(12.00M, 10.00M, 2.00M, 20);

            _handler
                .Setup(c => c.Handle(It.IsAny<CalculateValuesRequest>()))
                .Returns(expectedResponse);

            var response = _pricesController.Calculate(new CalculateValuesRequest("12", null, null, "20"));

            Assert.Multiple(() =>
            {
                var objectResult = (OkObjectResult)response;
                Assert.That(objectResult, Is.Not.Null);
                Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

                var content = objectResult.Value as CalculateValuesResponse;
                Assert.That(content, Is.Not.Null);
                Assert.That(content.GrossValue, Is.EqualTo(expectedResponse.GrossValue));
                Assert.That(content.NetValue, Is.EqualTo(expectedResponse.NetValue));
                Assert.That(content.VatValue, Is.EqualTo(expectedResponse.VatValue));
                Assert.That(content.VatRate, Is.EqualTo(expectedResponse.VatRate));
            });
        }
    }
}
