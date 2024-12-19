
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.behaviors
{
    public class LoggingBehaviors<TRequest, TResponse>
        (ILogger<LoggingBehaviors<TRequest,TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull,IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           logger.LogInformation("[START] handle request={} - Response = {Response} -RequestData={RequestData}",
               typeof(TRequest).Name,typeof(TResponse).Name,request);
            //tras registrar la solicitud entrante, podemos calcular la duracion de la solicitud y el tiempo de procesamientito
            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();
            var timerTaken= timer.Elapsed;
            if(timerTaken.Seconds>3) //if teh request is greather than 3 seconds,
                logger.LogWarning("[Performance] The request {Request} took {TimeTake}",
                    typeof(TRequest).Name,timerTaken.Seconds);

            logger.LogInformation("[END] Handle {Request} with {Response}", typeof(TRequest).Name,typeof (TResponse).Name);
            return response; //devolvemos el objeto responde a la siguiente operacion
        }
    }
}
