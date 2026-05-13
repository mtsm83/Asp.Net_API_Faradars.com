using Faradars.Application.DTOs.Management.Request.RequestService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Management.Request;

public interface IRequestService
{
    Task<Result<RequestDto>> CreateAsync(AddRequestDto dto, CancellationToken ct);
    Task<Result<RequestDto>> UpdateAsync(UpdateRequestDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteAsync(int requestId, CancellationToken ct);
    Task<Result<RequestDto>> GetByIdAsync(int requestId, CancellationToken ct);
    Task<Result<IReadOnlyList<RequestDto>>> GetAllAsync(CancellationToken ct);
    Task<Result<IReadOnlyList<RequestDto>>> 
        GetByCurrentUserAsync(CancellationToken ct);
}