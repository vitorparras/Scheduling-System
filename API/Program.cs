using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBuilder();  

var app = builder.Build();

app.ConfigureMiddlewares(); 
app.ConfigureEndpoints();

app.Run();