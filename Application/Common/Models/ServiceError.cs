namespace Application.Common.Models;

[Serializable]
public class ServiceError
{
    public ServiceError(string message, int code, string details)
    {
        this.Message = message;
        this.Code = code;
        this.Details = details;
    }

    public ServiceError() { }

    public string Message { get; }

    public int Code { get; }

    public string Details { get; set; }


    public static ServiceError ModelStateError(string validationError)
    {
        return new ServiceError(validationError, 998, "");
    }

    public static ServiceError CustomMessage(string errorMessage, int errorCode, string details)
    {
        return new ServiceError(errorMessage, errorCode, details);
    }

    public static ServiceError DefaultError => new ServiceError("An exception occured", 500, "");
    public static ServiceError ForbiddenError => new ServiceError("You are not authorized to call this action", 403, "");
    public static ServiceError NotFound => new ServiceError("The specified resource was not found", 404, "");
    public static ServiceError InvalidError => new ServiceError("Invalid request", 400, "");

    #region Override Equals Operator

    public override bool Equals(object obj)
    {
        var error = obj as ServiceError;
        return Code == error?.Code;
    }

    public bool Equals(ServiceError error)
    {
        return Code == error?.Code;
    }

    public override int GetHashCode()
    {
        return Code;
    }

    public static bool operator ==(ServiceError a, ServiceError b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if ((object)a == null || (object)b == null)
        {
            return false;
        }

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(ServiceError a, ServiceError b)
    {
        return !(a == b);
    }

    #endregion
}
