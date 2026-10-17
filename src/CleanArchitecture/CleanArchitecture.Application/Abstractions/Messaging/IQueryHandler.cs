using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging
{
    // TQuery es el metodo que se esta implementando
    // TResponse es el objeto que va a devolver
    public interface IQueryHandler<TQuery, TResponse> 
        : IRequestHandler<TQuery, Result<TResponse>> 
        where TQuery : IQuery<TResponse>
    {
    }
}
