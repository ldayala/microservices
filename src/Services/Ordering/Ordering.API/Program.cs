using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//------------------------------
//Infrastructure -- EF Core
//Application -- MediatR, AutoMapper, Validators
//API  -Carter , HealthChecks, Swagger ...

builder.Services
      .AddApplicationServices()
      .AddInfrastructureServices(builder.Configuration)
      .AddApiServices();

var app = builder.Build();
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}


app.Run();
