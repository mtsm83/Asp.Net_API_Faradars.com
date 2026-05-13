using System.Transactions;
using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Transaction;

public class Transaction : BaseEntity
{
    public string TransactionNumber { get; set; } // Unique transaction ID from our system
    public string? Description { get; set; }
    public int GatewayId { get; set; } // Which payment gateway was used?
    public int TypeId { get; set; } // wallet, purchase
    public string? Authority { get; set; } // for Iranian gateways
    public string? ReferenceId { get; set; } // Bank reference ID (شماره پیگیری)
    public string? CardNumber { get; set; } // (masked: 5022-29**-****-1234)
    public TransactionStatus Status { get; set; }
    public string? RequestData { get; set; } // json
    public string? ResponseData { get; set; } // json
    public string? CallbackData { get; set; }
    public string? VerificationData { get; set; }
    public string? ErrorMessage { get; set; }
    public string? IpAddress { get; set; } // User Ip while paying
    public DateTime InitiatedAt { get; set; }
    public DateTime? CompletedAt { get; set; } // When did gateway respond?
    public decimal TotalAmount { get; set; } // 100,000,000 Rials
    public string SystemCurrency { get; set; } = "IRR";
    
    public Gateway.Gateway Gateway { get; set; } // Which payment gateway was used?
    public TransactionType TransactionType { get; set; }
    public Order.Order Order { get; set; }
    public Transaction OriginalTransaction { get; set; }
}