using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Cinema.Application.Common.Behaviours;

public sealed class TimeTrackingLoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<TimeTrackingLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public TimeTrackingLoggingPipelineBehavior(ILogger<TimeTrackingLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_logger is null)
            return await next();

        Stopwatch stopwatch = Stopwatch.StartNew();

        var result = await next();

        stopwatch.Stop();
        _logger.LogInformation($"Request {request.GetType().Name} took {stopwatch.ElapsedMilliseconds} ms");

        return result;
    }
}
