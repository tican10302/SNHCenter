namespace REPOSITORY.Common;

public class ApiException : Exception
{
    public int StatusCode { get; set; }
    public string? Details { get; set; }

    public ApiException(int statusCode, string message, string? details = null) 
        : base(message)
    {
        StatusCode = statusCode;
        Details = details;
    }
}