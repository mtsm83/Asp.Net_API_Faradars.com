using Faradars.Application.DTOs.Payments.Transaction.TransactionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Transaction;

public interface ITransactionService
{
    Task<Result<TransactionDto>> AddTransactionAsync(AddTransactionDto dto, CancellationToken ct);
    Task<Result<TransactionDto>> UpdateTransactionAsync(UpdateTransactionDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteTransactionAsync(int transactionId, CancellationToken ct);
    Task<Result<TransactionDto>> GetTransactionByIdAsync(int transactionId, CancellationToken ct);
    Task<Result<List<TransactionDto>>> GetAllTransactionsAsync(CancellationToken ct);
}