using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBuilder();
builder.ConfigureAuthentication();
builder.ConfigureSwagger();


var app = builder.Build();

app.ConfigureMiddlewares();
app.ConfigureEndpoints();

app.Run();