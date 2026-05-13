using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Gateway;

public class PaymentGatewayConfiguration : BaseEntity
{
    public int GatewayId { get; set; }
    public string Environment { get; set; }
    public string MerchantId { get; set; }
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string RequestUrl { get; set; }
    public string VerifyUrl { get; set; }
    public string ReverseUrl { get; set; } // For refunds
    public string CallbackUrl { get; set; } // Callback URL after payment
    
    public Gateway Gateway { get; set; }
    
}