using DesafioNubank.Application;
using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.Interfaces;
using DesafioNubank.Application.Services;
using DesafioNubank.Domain.Repositories;
using DesafioNubank.Infrastructure.Data;
using DesafioNubank.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Para descoberta de endpoints pelo Swagger/OpenAPI

// Configuração do FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Registra validadores do assembly que contém ClienteCreateDtoValidator (ou outra classe da sua camada de Application)
builder.Services.AddValidatorsFromAssemblyContaining<ClienteCreateDtoValidator>(); // Use o namespace completo se necessário

// Configuração do AutoMapper
// Registra perfis de mapeamento do assembly que contém MappingProfile (ou outra classe da sua camada de Application)
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); // Use o namespace completo se necessário

// Configuração do Swagger (Swashbuckle)
builder.Services.AddSwaggerGen(options =>
{
    // Você pode adicionar configurações aqui, como informações do documento, segurança, etc.
    // Exemplo:
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{
         Version = "v1",
         Title = "Desafio Nubank API",
         Description = "API para o desafio técnico."
     });
});

// Configuração do DbContext com PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // "DefaultConnection" deve corresponder ao nome no appsettings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)); // UseNpgsql é para PostgreSQL com EF Core

// === REGISTRO DAS SUAS INTERFACES E IMPLEMENTAÇÕES ===
// Use AddScoped para serviços e repositórios que usam DbContext (que geralmente é Scoped)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Middleware para gerar o swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Nubank API V1"); // Exemplo de customização
        //options.RoutePrefix = string.Empty; // Para acessar o Swagger UI na raiz da aplicação (/)
    }); // Middleware para servir a UI do Swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();