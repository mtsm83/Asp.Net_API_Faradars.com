using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Wallet;

namespace Faradars.Domain.Entities.Payments.Transaction;


/// <summary>
/// 1 ) Deposit to wallet
/// 2 ) Withdraw from wallet
/// </summary>
public class WalletTransaction : BaseEntity
{
    public int WalletId { get; set; }
    public int WalletTransactionTypeId { get; set; }
    public int TransactionId { get; set; }
    
    public Wallet Wallet { get; set; }
    public Transaction Transaction { get; set; }
    public WalletTransactionType WalletTransactionType { get; set; }
}