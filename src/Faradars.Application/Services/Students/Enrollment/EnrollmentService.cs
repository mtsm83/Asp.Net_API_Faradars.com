using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.Application.Mappers.Students.Enrollment;
using Faradars.Domain.Entities.Management.Request.Refund;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Enrollment;

public class EnrollmentService(
    IRepository<Domain.Entities.Students.Enrollment.Enrollment> enrollmentRepository,
    IRepository<DismissalType> disTypeRepository,
    IRepository<User> userRepository,
    IRepository<RefundRequest> refundRequestRepository,
    IRepository<EnrollmentWithdrawal> enrollmentWithdrawalRepository,
    IRepository<EnrollmentDismissal> enrollmentDismissalRepository,
    IUserContextService userContextService) : IEnrollmentService, IScopedDependency
{
    
    public async Task<Result<EnrollmentDto>> EnrollUserAsync(EnrollUserDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<EnrollmentDto>(Error.NotFound);

        var newEnrollment = dto.MapAddToEnrollment();
        await enrollmentRepository.AddAsync(newEnrollment, ct);
        var enrollmentDto = newEnrollment.MapToEnrollmentDto();
        return Result.Success(enrollmentDto);
    }

    public async Task<Result<Unit>> DeleteEnrollmentRecordAsync(int enrollmentId, CancellationToken ct)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(ct, enrollmentId);
        if (enrollment == null)
            return Result.Failure<Unit>(Error.NotFound);
        await enrollmentRepository.DeleteAsync(enrollment, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> UnenrollUserAsync(int id, CancellationToken ct)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(ct, id);
        if (enrollment is null)
            return Result.Failure<Unit>(Error.NotFound);

        await enrollmentRepository.DeleteAsync(enrollment, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<bool>> HasAccessAsync(CheckAccessDto dto, CancellationToken ct)
    {
        var enrollment = await enrollmentRepository.TableNoTracking
            .Where(en => en.CourseId == dto.CourseId && en.StudentId == dto.UserId).FirstOrDefaultAsync(ct);

        if (enrollment is null)
            return Result.Success(false);

        return Result.Success(true);
    }

    public async Task<Result<List<EnrollmentDto>>> GetUserEnrollmentsAsync(int userId, CancellationToken ct)
    {
        var enrollments = await enrollmentRepository.TableNoTracking
            .Where(en => en.StudentId == userId).ToListAsync(ct);

        if (enrollments.Count < 1)
            return Result.Failure<List<EnrollmentDto>>(Error.NotFound);

        var enrollmentDtos = enrollments.Select(en => en.MapToEnrollmentDto()).ToList();
        return Result.Success(enrollmentDtos);
    }
    
}