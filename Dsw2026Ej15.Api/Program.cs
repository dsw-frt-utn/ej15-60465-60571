using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities; 
using Dsw2026Ej15.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();


builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapHealthChecks("/health-check");

app.MapControllers();

app.Run();

