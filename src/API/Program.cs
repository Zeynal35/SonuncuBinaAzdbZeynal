using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// AddControllers() artıq AddMyServices() içindədir - burada təkrar çağırmaq lazım deyil
builder.Services.AddMyServices(builder.Configuration);


var app = builder.Build();

// Middleware sırası DÜZELDİLDİ - ASP.NET Core standart pipeline
app.UseMyMiddlewares(); // Swagger, StaticFiles, HttpsRedirection

app.UseAuth(); // JWT token oxunur

app.MapControllers();

app.Run();
