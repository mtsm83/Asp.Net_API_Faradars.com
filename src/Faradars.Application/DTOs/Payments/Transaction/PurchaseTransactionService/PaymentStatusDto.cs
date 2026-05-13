namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class PaymentStatusDto
{
    public string Reference { get; init; }
    public string Status { get; init; }
    public decimal Amount { get; init; }
}