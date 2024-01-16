using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;


namespace MyTime.App.Abstractions.Behaviors;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : class, IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    if (!_validators.Any())
    {
      return await next();
    }

    var context = new ValidationContext<TRequest>(request);

    var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

    if (failures.Count != 0)
    {
      throw new Exceptions.ValidationException(failures);
    }

    return await next();
  }
}
