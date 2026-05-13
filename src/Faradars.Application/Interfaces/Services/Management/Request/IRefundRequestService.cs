using Faradars.Application.DTOs.Management.Request.RefundRequestService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Management.Request;

public interface IRefundRequestService
{
    Task<Result<RefundRequestDto>> CreateAsync(AddRefundRequestDto dto, CancellationToken ct);
    Task<Result<RefundRequestDto>> UpdateAsync(UpdateRefundRequestDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteAsync(int refundRequestId, CancellationToken ct);
    Task<Result<RefundRequestDto>> GetByIdAsync(int refundRequestId, CancellationToken ct);
    Task<Result<IReadOnlyList<RefundRequestDto>>> GetAllAsync(CancellationToken ct);
    Task<Result<IReadOnlyList<RefundRequestDto>>> 
        GetByCurrentUserAsync(CancellationToken ct);
}