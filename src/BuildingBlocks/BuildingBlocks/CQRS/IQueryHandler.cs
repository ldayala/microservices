using MediatR;


namespace BuildingBlocks.CQRS
{
   
    public interface IQueryHandler<in TQyery,TResponse>
        :IRequestHandler<TQyery,TResponse>
        where TQyery:IQuery<TResponse>
        where TResponse:notnull
    {
    }
}
