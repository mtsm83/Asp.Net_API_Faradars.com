using Faradars.Application.DTOs.Management.Request.RequestSubjectService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Management.Request;

public interface IRequestSubjectService
{
    Task<Result<SubjectDto>> CreateAsync(AddRequestSubjectDto dto, CancellationToken ct);
    Task<Result<SubjectDto>> UpdateAsync(UpdateRequestSubjectDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteAsync(int subjectId, CancellationToken ct);
    Task<Result<SubjectDto>> GetByIdAsync(int subjectId, CancellationToken ct);
    Task<Result<IReadOnlyList<SubjectDto>>> GetAllAsync(CancellationToken ct);
}