using MediatR;
using Microsoft.EntityFrameworkCore;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite")));


builder.Services.AddScoped<IAplicContaCorrente, AplicContaCorrente>();
builder.Services.AddScoped<IRepContaCorrente, RepContaCorrente>();
builder.Services.AddScoped<IAplicMovimento, AplicMovimento>();
builder.Services.AddScoped<IRepMovimento, RepMovimento>();
builder.Services.AddScoped<IMapperMovimento, MapperMovimento>();
builder.Services.AddScoped<IAplicIdempotencia, AplicIdempotencia>();
builder.Services.AddScoped<IRepIdempotencia, RepIdempotencia>();

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


