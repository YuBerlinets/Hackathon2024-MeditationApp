using meditationApp.Entities;

namespace meditationApp.Helpers;

public class Result<T>
{
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }

    public static Result<T> Success(T data, int statusCode = 200)
    {
        return new Result<T> { Data = data, StatusCode = statusCode };
    }

    public static Result<T> Failure(int statusCode, string errorMessage)
    {
        return new Result<T> { StatusCode = statusCode, ErrorMessage = errorMessage };
    }
}