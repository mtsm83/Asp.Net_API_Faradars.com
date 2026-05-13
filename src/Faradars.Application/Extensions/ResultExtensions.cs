using Faradars.Shared.Result;
using FluentValidation.Results;

namespace Faradars.Application.Extensions;

public static class ResultExtensions
{
    public static Result FailureFromFluentValidation(List<ValidationFailure> failures)
    {
        var combinedMessage = string.Join(" | ", failures.Select(f => f.ToString()));
        return Result.Failure(Error.FluentValidationError(combinedMessage));
    }
    
    public static Result<T> FailureFromFluentValidation<T>(List<ValidationFailure> failures)
    {
        var combinedMessage = string.Join(" | ", failures.Select(f => f.ToString()));
        return Result.Failure<T>(Error.FluentValidationError(combinedMessage));
    }
}