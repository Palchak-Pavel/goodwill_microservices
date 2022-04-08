using IncomePayments.API.Mongodb.Data;
using FluentValidation;
using IncomePayments.API.Mongodb;
using System.Reflection;
using EventBus.Messages.Common;
using FluentValidation.AspNetCore;
using IncomePayments.API.Consumers;
using MassTransit;

EntitiesConfig.Config();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(confg =>
{
    confg.AddConsumer<IncomeCreateConsumer>();
    confg.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://root:root@localhost:5672");
        cfg.ReceiveEndpoint(EventBusConstants.IncomesQueue, c =>
        {
            c.ConfigureConsumer<IncomeCreateConsumer>(ctx);
        });
    });
    
});

builder.Services.AddScoped<IMongoIncomePaymentContext, IncomePaymentContext>();
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
