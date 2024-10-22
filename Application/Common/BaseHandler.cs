using Application.Common.Models;
using FluentValidation.Results;
using System.Net;

namespace Application.Common;

public abstract class BaseHandler
{
    public ServiceResult<T> BadRequest<T>(ValidationResult validations, T data)
    {
        return ServiceResult.Failed(data, new ServiceError("Validation Error", (int)HttpStatusCode.BadRequest, string.Join("; ", validations.Errors.Select(x => x.ErrorMessage))));
    }
    public ServiceResult BadRequest(ValidationResult validations)
    {
        return ServiceResult.Failed("", new ServiceError("Validation Error", (int)HttpStatusCode.BadRequest, string.Join("; ", validations.Errors.Select(x => x.ErrorMessage))));
    }
    public ServiceResult BadRequest(string message)
    {
        return ServiceResult.Failed("", new ServiceError("Bad Request", (int)HttpStatusCode.BadRequest, message));
    }


    public ServiceResult<T> NotFound<T>(string entity, int entityId, T data)
    {
        return ServiceResult.Failed(data, new ServiceError("Entity not found", (int)HttpStatusCode.NotFound, $"{entity} with id {entityId} not found"));
    }
    public ServiceResult NotFound(string entity, string details)
    {
        return ServiceResult.Failed(new ServiceError($"Entity(es) not found ({entity})", (int)HttpStatusCode.NotFound, details));
    }
}
