using FluentValidation;
using MediatR;

namespace Cinema.Application.Common.Behaviours;

public sealed class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null || !_validators.Any())
            return await next();

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(error => error is not null);

        if (errors.Any())
            // TODO Map to 400
            throw new NotImplementedException();

        return await next();
    }
}
