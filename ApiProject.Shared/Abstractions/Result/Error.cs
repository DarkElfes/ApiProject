namespace ApiProject.Shared.Abstractions.Result;

public sealed record Error(string Code, ErrorType Type, string? Description = null)
{
    public static readonly Error None = new(string.Empty, ErrorType.None);
    public static readonly Error NullValue = new("Not have value", ErrorType.NullValue);

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public enum ErrorType
{
    None,
    NullValue,
    Validation,
    Conflict,
    NotFound,
    Problem
}