using API.Middlewares;

namespace API.Extensions;

public static class MiddlewareSetup
{
    public static void UseMyMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandling();
        app.UseRequestResponseLogging();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
