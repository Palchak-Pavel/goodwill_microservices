using FluentValidation;
using FluentValidation.AspNetCore;
using Incomes.API.Common;
using Incomes.API.Mongodb.Data;
using System.Reflection;

EntitiesConfig.Config();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMongoIncomeContext, IncomeContext>();

builder.Services.AddControllers();

builder.Services.AddFluentValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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

app.Run();
