using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Data.Repositories;
using MyApp.Application.Services;
using MyApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();

builder.Services.AddCustomSwagger();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Fornecedores API v2"));
app.UseRouting();
app.MapControllers();
app.Run();