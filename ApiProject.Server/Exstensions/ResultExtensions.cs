using ApiProject.Shared.Abstractions.Result;

namespace ApiProject.Server.Exstensions;

public static class ResultExtensions
{
    public static IResult Match(this Result result)
        => result.IsSuccess ? Results.Ok() : result.ToProblemDetails();

    public static IResult Match<T>(this Result<T> result)
        =>  result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();

    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert sucess result to problem");
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: result.Error.Code,
            //type: result.Error.Type,
            detail: result.Error.Description
            //extensions: new Dictionary<string, object?>
            //{
            //    { "errors", result.Error }
            //}
            );

        static int GetStatusCode(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}