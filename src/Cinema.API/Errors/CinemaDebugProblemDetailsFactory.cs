using Cinema.Application.Common.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Cinema.API.Errors;

public class CinemaDebugProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public CinemaDebugProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
                                                        int? statusCode = null,
                                                        string? title = null,
                                                        string? type = null,
                                                        string? detail = null,
                                                        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext!, problemDetails);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
                                                                            ModelStateDictionary modelStateDictionary,
                                                                            int? statusCode = null,
                                                                            string? title = null,
                                                                            string? type = null,
                                                                            string? detail = null,
                                                                            string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext!, problemDetails);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails)
    {
        if (_options.ClientErrorMapping.TryGetValue(problemDetails.Status!.Value, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        Exception? exception = httpContext?.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is not null)
        {
            problemDetails.Detail ??= exception.Message;

            problemDetails.Extensions["exception"] = new Dictionary<string, string?>
            {
                { "message", exception.Message },
                { "stackTrace", exception.StackTrace }
            };
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId is not null)
        {
            problemDetails.Extensions[ProblemDetailsExtensionsKeys.TraceId] = traceId;
        }

        // TODO Revisit
        //var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<IError>;
        //if (errors is not null)
        //{
        //    problemDetails.Extensions[ProblemDetailsExtensionsKeys.HandledErrors] = errors.Select(error => new { message = error.Message });
        //}
    }
}
