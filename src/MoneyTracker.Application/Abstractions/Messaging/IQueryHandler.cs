using MediatR;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;