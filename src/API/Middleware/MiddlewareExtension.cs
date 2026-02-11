
namespace API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}
