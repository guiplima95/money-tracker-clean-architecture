﻿using MediatR;
using Microsoft.Extensions.Logging;
using MoneyTracker.Application.Abstractions.Messaging;

namespace MoneyTracker.Application.Abstractions.Behaviors;

// Primary Constructor

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string name = request.GetType().Name;

        try
        {
            _logger.LogInformation("Executing command {Command}", name);

            TResponse? result = await next();

            _logger.LogInformation("Command {Command} processed successfully", name);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Command {Command} processing failed", name);

            throw;
        }
    }
}