using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Validation;

public interface IFluentValidatorService
{
    Task<Result<Unit>> ValidateAsync<T>(T dto, CancellationToken cancellationToken);
}