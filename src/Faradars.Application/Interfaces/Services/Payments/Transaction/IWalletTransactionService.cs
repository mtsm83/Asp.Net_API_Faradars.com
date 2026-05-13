using Faradars.Application.DTOs.Payments.Transaction.WalletTransactionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Transaction;

public interface IWalletTransactionService
{
    Task<Result<WalletTransactionDto>> AddWalletTransactionAsync(AddWalletTransactionDto dto, CancellationToken ct);
    Task<Result<WalletTransactionDto>> UpdateWalletTransactionAsync(UpdateWalletTransactionDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteWalletTransactionAsync(int transactionId, CancellationToken ct);
    Task<Result<WalletTransactionDto>> GetWalletTransactionByIdAsync(int transactionId, CancellationToken ct);
    Task<Result<List<WalletTransactionDto>>> GetAllWalletTransactionsAsync(CancellationToken ct);
}