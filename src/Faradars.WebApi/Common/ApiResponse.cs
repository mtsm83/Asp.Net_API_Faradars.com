namespace Faradars.WebApi.Common;

public class ApiResponse<T>
{
    public T? Data { get; init; }
    public bool IsSuccess { get; init; }
    public ApiErrorResponse? Error { get; init; }

    public ApiResponse(bool isSuccess, T? data, ApiErrorResponse? error)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static ApiResponse<T> Success(T data, string? message = null) => new(true, data, null);
    public static ApiResponse<T> Fail(string code, string message) => new(false, default, new ApiErrorResponse{Code = code, Message = message});
}

public class ApiErrorResponse
{
    public string Code { get; set; }
    public string Message { get; set; }
}