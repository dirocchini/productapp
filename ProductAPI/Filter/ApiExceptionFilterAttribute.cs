using Application.Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Filter;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;

    public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
       {
           { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
       };

        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        _logger.LogError("ERROR: " + context?.Exception?.Message);
        _logger.LogError("ERROR INNER: " + context?.Exception?.InnerException?.Message);


        if (_exceptionHandlers.TryGetValue(type, out var _) == true)
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }
    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = ServiceResult.Failed<string>(null, ServiceError.DefaultError);
        details.Error.Details = context?.Exception?.Message;


        if (!string.IsNullOrEmpty(context?.Exception?.InnerException?.Message))
            details.Error.Details += " | " + context?.Exception?.InnerException?.Message;

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = ServiceResult.Failed(ServiceError.ForbiddenError);

        context.Result = new UnauthorizedObjectResult(details);

        context.ExceptionHandled = true;
    }
}
