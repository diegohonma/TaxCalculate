using System.Net;
using TaxCalculate.Application.Exceptions;
using TaxCalculate.Application.Responses;

namespace TaxCalculate.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        int statusCode;
        var errorResponse = default(ErrorResponse);

        try
        {
            await _next(context);
            return;
        }
        catch (BusinessValidationException ex)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            errorResponse = new ErrorResponse(ex.Message);
        }

        catch (Exception ex)
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            errorResponse = new ErrorResponse(ex.Message);
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(errorResponse);
        await context.Response.CompleteAsync();
    }
}
