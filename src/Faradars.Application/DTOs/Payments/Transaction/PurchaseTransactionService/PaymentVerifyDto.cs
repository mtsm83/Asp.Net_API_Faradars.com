using Faradars.Application.Interfaces.General;

namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class PaymentVerifyDto: IDto
{
    public string Reference { get; init; }
    public bool IsSuccessful { get; init; }
    public string ProviderMessage { get; init; }
}