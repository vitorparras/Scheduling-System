using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.ConfigureAuthentication();
builder.ConfigureSwagger();

var app = builder.Build();

app.ConfigureMiddlewares();
app.ConfigureEndpoints();

app.Run();