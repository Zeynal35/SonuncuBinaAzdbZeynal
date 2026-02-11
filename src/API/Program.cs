using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMyServices(builder.Configuration);

var app = builder.Build();

app.UseMyMiddlewares();

app.Run();
