namespace Faradars.Application.DTOs.Users.Wallet.WalletService;

public class WalletDto
{
    public int WalletId { get; set; }
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public decimal TotalDeposited { get; set; }
    public decimal TotalWithdrawn { get; set; }
    public decimal TotalSpent { get; set; }
}