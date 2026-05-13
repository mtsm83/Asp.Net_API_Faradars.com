namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class CreatePaymentDto
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public int UserId { get; init; }
    public int? CourseId { get; init; }
    public string SuccessUrl { get; init; }
    public string CancelUrl { get; init; }
    
}