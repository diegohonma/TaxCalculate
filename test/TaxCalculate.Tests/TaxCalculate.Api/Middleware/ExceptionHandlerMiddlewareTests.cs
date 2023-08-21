using Microsoft.AspNetCore.Http;
using TaxCalculate.Api.Middleware;
using TaxCalculate.Application.Exceptions;

namespace TaxCalculate.Tests.TaxCalculate.Api.Middleware
{
    internal class ExceptionHandlerMiddlewareTests
    {
        [Test]
        public async Task Should_ReturnBadRequest_When_ValidationException()
        {
            var exceptionHandlerMiddleware = new ExceptionHandlerMiddleware(httpContext => Task.FromException(new BusinessValidationException("error")));
            var context = new DefaultHttpContext();

            await exceptionHandlerMiddleware.InvokeAsync(context);

            Assert.That(context.Response.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task Should_ReturnInternalServerError_When_Exception()
        {
            var exceptionHandlerMiddleware = new ExceptionHandlerMiddleware(httpContext => Task.FromException(new Exception("error")));
            var context = new DefaultHttpContext();

            await exceptionHandlerMiddleware.InvokeAsync(context);

            Assert.That(context.Response.StatusCode, Is.EqualTo(500));
        }
    }
}
