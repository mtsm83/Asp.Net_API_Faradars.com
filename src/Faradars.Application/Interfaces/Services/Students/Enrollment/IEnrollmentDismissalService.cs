using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Enrollment;

public interface IEnrollmentDismissalService
{
    Task<Result<EnrollmentDismissalDto>> AddEnrollmentDismissalAsync(DismissEnrollmentDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteEnrollmentDismissalAsync(int dismissalId, CancellationToken ct);
    Task<Result<EnrollmentDismissalDto>> GetEnrollmentDismissalAsync(int enrollmentId, CancellationToken ct);
    Task<Result<List<EnrollmentDismissalDto>>> GetAllEnrollmentDismissalAsync(CancellationToken ct);
}