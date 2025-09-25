using BuildingBlocks.behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.Masstransit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();

//Application services
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    conf.AddOpenBehavior(typeof(LoggingBehaviors<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

//Data services
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);//para que el username sea el id cuando marten crea la tabla
    //opts.AutoCreateSchemaObjects = AutoCreate.All;
}).UseLightweightSessions();
#region
/* ESTO NO FUNCIONARIA PORQUE TOMARIA LA SEGUNDA */
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//builder.Services.AddScoped<IBasketRepository, CacheBasketRepository>();
//con sructor library
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();
//para ello decoramos las clase gracias scrutor library
/*esto sin scrutor libreri*
builder.Services.AddScoped<IBasketRepository>(provider =>
{
    var basketRepostory = provider.GetRequiredService<IBasketRepository>();
    return new CacheBasketRepository(basketRepostory, provider.GetRequiredService<IDistributedCache>());
});
*/
//registramos redis
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});
#endregion

// GRPC Services
builder.Services.AddGrpcClient<Discount.Grpc.DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});


//asyn communication service
//builder.Services.AddMesageBroker(builder.Configuration);

//Cross-cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!,name:"postgres")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!,name:"redis");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.UseExceptionHandler(options => { });
app.UseHttpsRedirection();

app.UseHealthChecks("/health",
    new HealthCheckOptions { 
    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse  //esto nos devuelve una repuesta json 
    });


app.Run();


