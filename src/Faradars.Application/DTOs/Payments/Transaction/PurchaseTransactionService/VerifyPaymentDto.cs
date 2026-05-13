using Faradars.Application.Interfaces.General;

namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class VerifyPaymentDto: IDto
{
    public string Reference { get; init; }
    public string ProviderTransactionId { get; init; }
}