namespace API.RequestHelpers;
public class ApiErrorResponse
{
    public ApiErrorResponse(string status, string message)
    {
        Status = status;
        Message = message;
    }
    public string Status { get; set; }
    public string Message { get; set; }

    public static ApiErrorResponse Response(string status, string message)
    {
        return new ApiErrorResponse(status, message);
    }
}