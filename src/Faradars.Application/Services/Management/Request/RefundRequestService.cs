using Faradars.Application.DTOs.Management.Request.RefundRequestService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.Application.Mappers.Management.Request;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Domain.Entities.Management.Request.Refund;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Management.Request;

public class RefundRequestService(
    IRepository<RefundRequest> refundRepository,
    IRepository<UserRequest> requestRepository,
    IUserContextService userContext) : IRefundRequestService
{
    public async Task<Result<RefundRequestDto>> CreateAsync(
        AddRefundRequestDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var request = await requestRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == dto.RequestId, ct);

        if (request is null)
            return Result.Failure<RefundRequestDto>(Error.NotFound);

        if (request.UserId != currentUserId)
            return Result.Failure<RefundRequestDto>(Error.Unauthorized);
        var refund = dto.MapAddRefundRequest(currentUserId);
        // new RefundRequest
        // {
        //     RequestId = dto.RequestId,
        //     Reason = dto.Reason,
        //     Description = dto.Description,
        //     Status = RefundStatus.Pending,
        //     CreatedAt = DateTime.UtcNow,
        //     CreatedBy = currentUserId
        // };
        await refundRepository.AddAsync(refund, ct);
        return Result.Success(refund.MapToRefundRequestDto());
    }

    public async Task<Result<RefundRequestDto>> UpdateAsync(
        UpdateRefundRequestDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;
        var refund = await refundRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == dto.RefundId, ct);

        if (refund is null)
            return Result.Failure<RefundRequestDto>(Error.NotFound);

        if (refund.CreatedBy != currentUserId)
            return Result.Failure<RefundRequestDto>(Error.Unauthorized);
        refund.MapUpdateRefundRequest(dto, currentUserId);
        // refund.Reason = dto.Reason;
        // refund.Description = dto.Description;
        // refund.UpdatedAt = DateTime.UtcNow;
        refund.UpdatedBy = currentUserId;
        return Result.Success(refund.MapToRefundRequestDto());
    }

    public async Task<Result<Unit>> DeleteAsync(
        int refundRequestId,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;
        var refund = await refundRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == refundRequestId, ct);
        if (refund is null)
            return Result.Failure<Unit>(Error.NotFound);

        if (refund.CreatedBy != currentUserId)
            return Result.Failure<Unit>(Error.Unauthorized);
        await refundRepository.DeleteAsync(refund, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<RefundRequestDto>> GetByIdAsync(
        int refundRequestId,
        CancellationToken ct)
    {
        var refund = await refundRepository
            .Table
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == refundRequestId && !r.IsDeleted, ct);

        if (refund is null)
            return Result.Failure<RefundRequestDto>(Error.NotFound);

        return Result.Success(refund.MapToRefundRequestDto());
    }

    public async Task<Result<IReadOnlyList<RefundRequestDto>>> GetAllAsync(
        CancellationToken ct)
    {
        var refunds = await refundRepository
            .Table
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => r.MapToRefundRequestDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<RefundRequestDto>>(refunds);
    }

    public async Task<Result<IReadOnlyList<RefundRequestDto>>> GetByCurrentUserAsync(
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var refunds = await refundRepository
            .Table
            .AsNoTracking()
            .Where(r => r.CreatedBy == currentUserId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => r.MapToRefundRequestDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<RefundRequestDto>>(refunds);
    }
}