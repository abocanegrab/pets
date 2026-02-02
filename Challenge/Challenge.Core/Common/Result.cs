namespace Challenge.Core.Common;

public class Result<T>
{
    public T Data { get; }

    private Result(T data)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data);
    }
}
