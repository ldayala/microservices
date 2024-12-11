using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    //esta interfaz esta diseñada para devolver un resultado y la utilizamos para las operaciones de lectura
    public interface IQuery<out TResponse>:IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
