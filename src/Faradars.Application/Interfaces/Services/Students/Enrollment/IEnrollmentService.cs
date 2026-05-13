using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Enrollment;

public interface IEnrollmentService
{
    Task<Result<EnrollmentDto>> EnrollUserAsync(EnrollUserDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteEnrollmentRecordAsync(int enrollmentId, CancellationToken ct);
    Task<Result<bool>> HasAccessAsync(CheckAccessDto dto, CancellationToken ct);
    Task<Result<List<EnrollmentDto>>> GetUserEnrollmentsAsync(int userId, CancellationToken ct);
}