using MediatR;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;