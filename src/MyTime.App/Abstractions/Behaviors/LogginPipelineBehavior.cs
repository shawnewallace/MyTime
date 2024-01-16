using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MyTime.App.Abstractions.Behaviors;

public class LogginPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	// where TResponse : Result
{
	private readonly ILogger<LogginPipelineBehavior<TRequest, TResponse>> _logger;

	public LogginPipelineBehavior(ILogger<LogginPipelineBehavior<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		_logger.LogInformation(
			"Starting request {@RequestName}, {@DateTimeUtc}",
			typeof(TRequest).Name,
			DateTime.UtcNow);

		var result = await next();

		_logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}",
			typeof(TRequest).Name,
			DateTime.UtcNow);

		return result;
	}
}