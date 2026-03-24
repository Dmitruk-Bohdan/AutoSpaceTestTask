using AutoSpaceTestTask.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
    services.AddWeb();

var app = builder.Build();
app.ConfigureMiddleware();

app.Run();
