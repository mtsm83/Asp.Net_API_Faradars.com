using Faradars.Application.Extensions;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Validation;
using Faradars.Shared.Result;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Faradars.Application.Services.Validation;

public class FluentValidatorService(IServiceProvider serviceProvider) : IFluentValidatorService, IScopedDependency
{
    public async Task<Result<Unit>> ValidateAsync<T>(T dto, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();
        if (validator is null)
            return Result.Failure<Unit>(Error.ValidatorNotFound);

        var result = await validator.ValidateAsync(dto, cancellationToken);
        return result.IsValid
            ? Result.Success(Unit.Value)
            : ResultExtensions.FailureFromFluentValidation<Unit>(result.Errors);
    }
}
