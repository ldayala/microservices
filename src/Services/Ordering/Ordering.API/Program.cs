using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

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

app.MapGet("/", () => "Hello World!");

app.Run();
