using MediatR;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;