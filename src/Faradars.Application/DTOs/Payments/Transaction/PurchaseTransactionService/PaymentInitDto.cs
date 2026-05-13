namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class PaymentInitDto
{
    public string PaymentUrl { get; init; }
    public string Reference { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
}