using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.Application.Mappers.Students.Enrollment;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Enrollment;

public class DismissalTypeService(
    IRepository<DismissalType> disTypeRepository,
    IUserContextService userContextService) : IDismissalTypeService, IScopedDependency
{
    public async Task<Result<DismissalTypeDto>> AddDismissalTypeAsync(AddDismissalTypeDto dto, CancellationToken ct)
    {
        var dismissalType = new DismissalType();
        dismissalType.Name = dto.Name;
        dismissalType.Description = dto.Description;
        dismissalType.CreatedBy = userContextService.CurrentUser.UserId;
        await disTypeRepository.AddAsync(dismissalType, ct);
        var dismissalTypeDto = dismissalType.MapToDismissalTypeDto();
        return Result.Success(dismissalTypeDto);
    }

    public async Task<Result<Unit>> DeleteDismissalTypeAsync(int dismissalId, CancellationToken ct)
    {
        var dismissalType = await disTypeRepository.GetByIdAsync(ct, dismissalId);
        if (dismissalType == null)
            return Result.Failure<Unit>(Error.NotFound);
        await disTypeRepository.DeleteAsync(dismissalType, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<DismissalTypeDto>> GetDismissalTypeByIdAsync(int dismissalId, CancellationToken ct)
    {
        var dismissalType = await disTypeRepository.GetByIdAsync(ct, dismissalId);
        if (dismissalType == null)
            return Result.Failure<DismissalTypeDto>(Error.NotFound);

        var dismissalTypeDto = dismissalType.MapToDismissalTypeDto();
        return Result.Success(dismissalTypeDto);
    }

    public async Task<Result<List<DismissalTypeDto>>> GetAllDismissalTypesAsync(CancellationToken ct)
    {
        var dismissalTypes = await disTypeRepository.TableNoTracking.ToListAsync(ct);
        if (dismissalTypes.Count == 0)
            return Result.Failure<List<DismissalTypeDto>>(Error.NotFound);
        var dismissalTypeDtos = dismissalTypes.Select(dt => dt.MapToDismissalTypeDto()).ToList();
        return Result.Success<List<DismissalTypeDto>>(dismissalTypeDtos);
    }
}