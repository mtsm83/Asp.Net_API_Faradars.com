using Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Transaction;

public interface IPurchaseTransactionService
{
    Task<Result<PurchaseTransactionDto>> AddPurchaseTransactionAsync(AddPurchaseTransactionDto dto, CancellationToken ct);
    Task<Result<PurchaseTransactionDto>> UpdatePurchaseTransactionAsync(UpdatePurchaseTransactionDto dto, CancellationToken ct);
    Task<Result<Unit>> DeletePurchaseTransactionAsync(int transactionId, CancellationToken ct);
    Task<Result<PurchaseTransactionDto>> GetPurchaseTransactionByIdAsync(int transactionId, CancellationToken ct);
    Task<Result<List<PurchaseTransactionDto>>> GetAllPurchaseTransactionsAsync(CancellationToken ct);
}