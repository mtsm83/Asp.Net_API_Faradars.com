using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.Application.Mappers.Students.Enrollment;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Enrollment;

public class EnrollmentDismissalService(
    IRepository<Domain.Entities.Students.Enrollment.Enrollment> enrollmentRepository,
    IRepository<DismissalType> disTypeRepository,
    IRepository<EnrollmentDismissal> enrollmentDismissalRepository,
    IUserContextService userContextService) : IEnrollmentDismissalService, IScopedDependency
{
        public async Task<Result<EnrollmentDismissalDto>> AddEnrollmentDismissalAsync(DismissEnrollmentDto dto,
        CancellationToken ct)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(ct, dto.EnrollmentId);
        if (enrollment is null)
            return Result.Failure<EnrollmentDismissalDto>(Error.NotFound);
        
        var dismissalType = await disTypeRepository.GetByIdAsync(ct, dto.DismissalTypeId);
        if (dismissalType is null)
            return Result.Failure<EnrollmentDismissalDto>(Error.NotFound);

        var newEnrollmentDismissal = new EnrollmentDismissal();
        newEnrollmentDismissal.MapDismissEnrollment(dto);
        newEnrollmentDismissal.CreatedBy = userContextService.CurrentUser.UserId;
        await enrollmentDismissalRepository.AddAsync(newEnrollmentDismissal, ct);
        var enrollmentDismissalDto = newEnrollmentDismissal.MapToEnDismissalDto();
        return Result.Success(enrollmentDismissalDto);
    }

    public async Task<Result<Unit>> DeleteEnrollmentDismissalAsync(int dismissalId, CancellationToken ct)
    {
        var dismissal = await enrollmentDismissalRepository.GetByIdAsync(ct, dismissalId);
        if (dismissal == null)
            return Result.Failure<Unit>(Error.NotFound);
        await enrollmentDismissalRepository.DeleteAsync(dismissal, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<EnrollmentDismissalDto>> GetEnrollmentDismissalAsync(int enrollmentId, CancellationToken ct)
    {
        var dismissal = await enrollmentDismissalRepository.TableNoTracking
            .FirstOrDefaultAsync(en => en.EnrollmentId == enrollmentId, ct);
        if (dismissal == null)
            return Result.Failure<EnrollmentDismissalDto>(Error.NotFound);
        var dismissalDto = dismissal.MapToEnDismissalDto();
        return Result.Success(dismissalDto);
    }

    public async Task<Result<List<EnrollmentDismissalDto>>> GetAllEnrollmentDismissalAsync(CancellationToken ct)
    {
        var dismissals = await enrollmentDismissalRepository.TableNoTracking.ToListAsync(ct);
        if (dismissals.Count == 0)
            return Result.Failure<List<EnrollmentDismissalDto>>(Error.NotFound);
        var dismissalDtos = dismissals.Select(d => d.MapToEnDismissalDto()).ToList();
        return Result.Success(dismissalDtos);
    }
}