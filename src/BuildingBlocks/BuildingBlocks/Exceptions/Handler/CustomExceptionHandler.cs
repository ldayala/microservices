
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler
        (ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        //este metodo va a interceptar todas las excepciones para poderla manejar
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
                "Error message :{exceptionMessage}, Time of current {time}",
                exception.Message, DateTime.UtcNow
                );
            // vamos a identificar el tipo de exception para rellenar los detalles de los valores de estas
            (string Detail, string title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ =>
                 (
                 exception.Message,
                 exception.GetType().Name,
                 httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                 )

            };

            var problemDetails = new ProblemDetails
            {
                Title = details.title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };
            //creo una extension de la clase para agergarle otra propiedad
            problemDetails.Extensions.Add("TraceId",httpContext.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErros", validationException.Errors); 
            }
            await httpContext.Response.WriteAsJsonAsync( problemDetails,cancellationToken );
            return true;
        }
    }
}
