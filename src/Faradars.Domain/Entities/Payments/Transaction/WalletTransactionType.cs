using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Transaction;

public class WalletTransactionType: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}