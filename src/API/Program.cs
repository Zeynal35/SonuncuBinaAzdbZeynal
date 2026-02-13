using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// AddControllers() artıq AddMyServices() içindədir - burada təkrar çağırmaq lazım deyil
builder.Services.AddMyServices(builder.Configuration);

var app = builder.Build();

// Middleware sırası DÜZELDİLDİ - ASP.NET Core standart pipeline
app.UseMyMiddlewares(); // Swagger, StaticFiles, HttpsRedirection

app.UseAuthentication(); // JWT token oxunur
app.UseAuthorization();  // [Authorize] yoxlanılır

app.MapControllers();

app.Run();
