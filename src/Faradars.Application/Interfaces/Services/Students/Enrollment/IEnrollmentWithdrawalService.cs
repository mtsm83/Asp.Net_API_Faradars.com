using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Students.Enrollment;

public interface IEnrollmentWithdrawalService
{
    Task<Result<WithdrawalDto>> AddWithdrawalAsync(AddWithdrawalDto dto, CancellationToken ct); // related to refundRequest & refund transactions
    Task<Result<Unit>> DeleteWithdrawalAsync(int withdrawalId, CancellationToken ct);
    Task<Result<WithdrawalDto>> GetWithdrawalByIdAsync(int withdrawalId, CancellationToken ct);
    Task<Result<List<WithdrawalDto>>> GetAllUserWithdrawalsAsync(int userId, CancellationToken ct);

}