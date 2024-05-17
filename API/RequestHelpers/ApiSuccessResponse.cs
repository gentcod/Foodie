namespace API.RequestHelpers;
public class ApiSuccessResponse<T>
{
    public ApiSuccessResponse(string status, string message, T data)
    {

        Status = status;
        Message = message;
        Data = data;
    }
    public string Status { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public static ApiSuccessResponse<T> Response(string status, string message, T data)
    {
        return new ApiSuccessResponse<T>(status, message, data);
    }
}