namespace Application.Shared.Helpers;

public class BaseResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public static BaseResponse<T> Ok(T? data=default, string? message = null)
   => new() { Data = data, IsSuccess = true, Message = message };
    public static BaseResponse<T> Fail(string? message = null)
        => new() { Data = default, IsSuccess = false, Message = message };
}

public class BaseResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public static BaseResponse Ok(string? message = null)
       => new() { IsSuccess = true, Message = message };
    public static BaseResponse Fail(string? message = null)
        => new() { IsSuccess = false, Message = message };
}