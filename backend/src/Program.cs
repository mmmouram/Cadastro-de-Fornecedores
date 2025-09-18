using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using myApp.Config;
using myApp.Context;
using myApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Carrega configurações
var configuration = builder.Configuration;

// Adiciona Controllers
builder.Services.AddControllers();

// Configura o contexto com SQL Server
builder.Services.AddDbContext<FornecedorDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Registra as dependências da aplicação
builder.Services.AddInfraServices();

var app = builder.Build();

app.UseRouting();

// Middleware de auditoria para log de requisições
app.UseMiddleware<AuditoriaMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();