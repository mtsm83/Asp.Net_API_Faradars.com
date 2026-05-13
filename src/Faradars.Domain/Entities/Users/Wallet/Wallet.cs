using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Payments.Transaction;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Users.Wallet;

public class Wallet : BaseEntity
{
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalDeposited { get; set; }
    public decimal TotalWithdrawn { get; set; }
    public decimal TotalSpent { get; set; }
    public User User { get; set; } = null!;
    public ICollection<WalletTransaction> Transactions { get; set; }= new List<WalletTransaction>();
}