namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
