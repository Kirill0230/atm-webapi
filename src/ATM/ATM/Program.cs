using ATM.Application;
using ATM.Infrastructure;
using ATM.Presentation.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? systemPassword = builder.Configuration["AdminSession:SystemPassword"];
if (string.IsNullOrEmpty(systemPassword))
{
    throw new Exception("System password not set");
}

builder.Services
    .AddApplication(systemPassword)
    .AddInfrastructureRepositories()
    .AddPresentationHttp();

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();