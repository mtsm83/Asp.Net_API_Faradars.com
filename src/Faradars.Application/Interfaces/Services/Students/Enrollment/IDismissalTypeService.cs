using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Enrollment;

public interface IDismissalTypeService
{
    Task<Result<DismissalTypeDto>> AddDismissalTypeAsync(AddDismissalTypeDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteDismissalTypeAsync(int dismissalId, CancellationToken ct);
    Task<Result<DismissalTypeDto>> GetDismissalTypeByIdAsync(int dismissalId, CancellationToken ct);
    Task<Result<List<DismissalTypeDto>>> GetAllDismissalTypesAsync(CancellationToken ct);

}