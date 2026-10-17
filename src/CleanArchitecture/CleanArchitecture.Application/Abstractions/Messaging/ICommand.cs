using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {

    }

    // Interface comun para poder agregar a futuro constraints de verificacion de interfaces o componentes
    public interface IBaseCommand
    {

    }
}
