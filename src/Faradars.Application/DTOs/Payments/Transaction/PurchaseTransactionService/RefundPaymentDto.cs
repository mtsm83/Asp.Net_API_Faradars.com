using Faradars.Application.Interfaces.General;

namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class RefundPaymentDto: IDto
{
    public string Reference { get; init; }
    public decimal Amount { get; init; }
}