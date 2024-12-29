using PunchCardApp.Models;

namespace PunchCardApp.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult
        {
            Message = "Success!",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message ?? "Success!",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Failed",
            StatusCode = StatusCode.ERROR
        };
    }
    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not Found",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Already Exists",
            StatusCode = StatusCode.EXISTS
        };
    }
}