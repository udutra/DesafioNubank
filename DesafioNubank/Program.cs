using DesafioNubank.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configurar o DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // "DefaultConnection" deve corresponder ao nome no appsettings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)); // UseNpgsql é para PostgreSQL com EF Core


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();