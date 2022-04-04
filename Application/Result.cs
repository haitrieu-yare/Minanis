namespace Application;

public class Result<T>
{
    public T? Value { get; private init; }
    public bool IsSuccess { get; private init; }
    public bool IsUnauthorized { get; private init; }
    public Pagination? Pagination { get; private init; }
    public string? NewResourceId { get; private init; }
    public string? SuccessMessage { get; private init; }
    public string? ErrorMessage { get; private init; }
    public string? NotFoundMessage { get; private init; }
    public string? UnauthorizedMessage { get; private init; }

    public static Result<T> Success(T value, string successMessage) => new()
    {
        IsSuccess = true,
        Value = value,
        SuccessMessage = successMessage
    };

    public static Result<T> Success(T value, Pagination pagination, string successMessage) => new()
    {
        IsSuccess = true,
        Value = value,
        Pagination = pagination,
        SuccessMessage = successMessage
    };

    public static Result<T> Created(T value, string newResourceId, string successMessage) => new()
    {
        IsSuccess = true,
        Value = value,
        NewResourceId = newResourceId,
        SuccessMessage = successMessage
    };

    public static Result<T> Failure(string errorMessage) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage
    };

    public static Result<T> NotFound(string notFoundMessage) => new()
    {
        IsSuccess = false,
        NotFoundMessage = notFoundMessage
    };

    public static Result<T> Unauthorized(string unauthorizedMessage) => new()
    {
        IsSuccess = false,
        IsUnauthorized = true,
        UnauthorizedMessage = unauthorizedMessage
    };
}