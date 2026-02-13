using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // ⭐ BU MÜTLƏQDİR
builder.Services.AddMyServices(builder.Configuration);

var app = builder.Build();

app.UseMyMiddlewares();

app.UseAuth();   // auth middleware

app.MapControllers();

app.Run();
