using System.Net;
using System.Text.Json;
using FluentValidation;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteResponse(context, 400, "Validation error", ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (KeyNotFoundException ex)
        {
            await WriteResponse(context, 404, ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            await WriteResponse(context, 401, "Unauthorized");
        }
        catch (Exception ex)
        {
            await WriteResponse(context, 500, "Internal Server Error", ex.Message);
        }
    }

    private async Task WriteResponse(HttpContext context, int statusCode, string message, object? detail = null)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode,
            message,
            detail
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
