
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;


namespace BuildingBlocks.behaviors
{//una vez implementado hay que registralo en la inyeccion de depencias en Progran.cs dentro de la configuracion de mediaTr
    public class ValidationBehaviors<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators) //inyectamos el objetos validator para obtener los errores
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>//esto es para que esta regla solo se aplique a los TCommand
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult=
                await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context, cancellationToken)));

            var failures=
                validationResult
                .Where(r=>r.Errors.Any())
                .SelectMany(r=>r.Errors)
                .ToList();

            if (failures.Any()) {
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
