using BuildingBlocks.behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.



//app.UseHttpsRedirection();


app.MapCarter();

app.Run();

