using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.Application.Mappers.Students.Enrollment;
using Faradars.Domain.Entities.Management.Request.Refund;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Students.Enrollment;

public class EnrollmentWithdrawalService(
    IRepository<Domain.Entities.Students.Enrollment.Enrollment> enrollmentRepository,
    IRepository<RefundRequest> refundRequestRepository,
    IRepository<EnrollmentWithdrawal> enrollmentWithdrawalRepository,
    IUserContextService userContextService) : IEnrollmentWithdrawalService, IScopedDependency
{
    public async Task<Result<WithdrawalDto>> AddWithdrawalAsync(AddWithdrawalDto dto, CancellationToken ct)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(ct, dto.EnrollmentId);
        if (enrollment == null)
            return Result.Failure<WithdrawalDto>(Error.NotFound);
        var refundRequest = await refundRequestRepository.GetByIdAsync(ct, dto.RefundRequestId);
        if (refundRequest == null)
            return Result.Failure<WithdrawalDto>(Error.NotFound);

        var newWithdrawal = new EnrollmentWithdrawal
        {
            EnrollmentId = dto.EnrollmentId,
            RefundRequestId = dto.RefundRequestId,
            WithdrawalReason = dto.WithdrawalReason,
            CreatedBy = userContextService.CurrentUser.UserId,
        };
        await enrollmentWithdrawalRepository.AddAsync(newWithdrawal, ct);
        var withdrawalDto = new WithdrawalDto
        {
            WithdrawalId = newWithdrawal.Id,
            EnrollmentId = newWithdrawal.EnrollmentId,
            RefundRequestId = newWithdrawal.RefundRequestId,
            WithdrawalReason = newWithdrawal.WithdrawalReason,
            CreatedAt = newWithdrawal.CreatedAt,
            CreatorId = newWithdrawal.CreatedBy,
        };
        return Result.Success(withdrawalDto);
    }

    public async Task<Result<Unit>> DeleteWithdrawalAsync(int withdrawalId, CancellationToken ct)
    {
        var withdrawal = await enrollmentWithdrawalRepository.GetByIdAsync(ct, withdrawalId);
        if (withdrawal == null)
            return Result.Failure<Unit>(Error.NotFound);
        await enrollmentWithdrawalRepository.DeleteAsync(withdrawal, ct);
        withdrawal.DeletedBy = userContextService.CurrentUser.UserId;
        return Result.Success(Unit.Value);
    }

    public async Task<Result<WithdrawalDto>> GetWithdrawalByIdAsync(int withdrawalId, CancellationToken ct)
    {
        var withdrawal = await enrollmentWithdrawalRepository.GetByIdAsync(ct, withdrawalId);
        if (withdrawal == null)
            return Result.Failure<WithdrawalDto>(Error.NotFound);
        var withdrawalDto = withdrawal.MapToWithdrawalDto();
        return Result.Success(withdrawalDto);
    }

    public async Task<Result<List<WithdrawalDto>>> GetAllUserWithdrawalsAsync(int userId, CancellationToken ct)
    {
        var withdrawals = await enrollmentWithdrawalRepository.TableNoTracking
            .Include(w => w.Enrollment)
            .Where(w => w.Enrollment.StudentId == userId)
            .ToListAsync(ct);

        if (withdrawals.Count == 0)
            return Result.Failure<List<WithdrawalDto>>(Error.NotFound);
        var withdrawalDtos = withdrawals.Select(w => w.MapToWithdrawalDto()).ToList();
        return Result.Success(withdrawalDtos);
    }

}