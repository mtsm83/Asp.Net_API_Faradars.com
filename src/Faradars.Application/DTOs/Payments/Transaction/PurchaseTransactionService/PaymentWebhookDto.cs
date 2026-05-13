using Faradars.Application.Interfaces.General;

namespace Faradars.Application.DTOs.Payments.Transaction.PurchaseTransactionService;

public class PaymentWebhookDto: IDto
{
    public string Provider { get; init; }
    public string Payload { get; init; }
}