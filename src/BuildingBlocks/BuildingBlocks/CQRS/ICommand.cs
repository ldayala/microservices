using MediatR;

namespace BuildingBlocks.CQRS
{
    //esta interfaz no devuelve respuesta
    public interface ICommand : ICommand<Unit>
    {

    }
    //esta interfas devuelve una respuesta
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {
    }
}
