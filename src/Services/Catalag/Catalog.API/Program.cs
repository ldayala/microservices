
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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

//añadimos el manejador de excepciones globales la inyeccion de dependencias
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.



//app.UseHttpsRedirection();


app.MapCarter();
#region Manejado exepcione
// para usar el manejador de exception global
/*lo hemos implementado mejorado en BuildingBlock/Exception/Hanlder
 * app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        //obtenemos la excepcion
        var exception= context.Features.Get<IExceptionHandlerFeature>() ?.Error;
        if (exception == null)
            return;
        
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace,
          
        };
        
        var logger= context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception,exception.Message);

        context.Response.StatusCode =StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});*/


#endregion
//configuramos un pipeline para que funciones el sevicio de manejador de excepcioons
app.UseExceptionHandler(options => { });
app.Run();

