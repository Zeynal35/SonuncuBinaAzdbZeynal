using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new
            {
                field = e.PropertyName,
                message = e.ErrorMessage
            });

            await WriteResponse(
                context,
                HttpStatusCode.BadRequest,
                "Validation error",
                errors
            );
        }
        catch (KeyNotFoundException ex)
        {
            await WriteResponse(
                context,
                HttpStatusCode.NotFound,
                ex.Message
            );
        }
        catch (UnauthorizedAccessException)
        {
            await WriteResponse(
                context,
                HttpStatusCode.Unauthorized,
                "Unauthorized"
            );
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error");

            await WriteResponse(
                context,
                HttpStatusCode.BadRequest,
                "Database error",
                ex.InnerException?.Message ?? ex.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteResponse(
                context,
                HttpStatusCode.InternalServerError,
                "Internal Server Error",
                ex.InnerException?.Message ?? ex.Message
            );
        }
    }

    private async Task WriteResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string message,
        object? detail = null)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode = (int)statusCode,
            message,
            detail
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}
