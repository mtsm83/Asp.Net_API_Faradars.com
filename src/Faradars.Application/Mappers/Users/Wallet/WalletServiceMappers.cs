using Faradars.Application.DTOs.Users.Wallet.WalletService;

namespace Faradars.Application.Mappers.Users.Wallet;

public static class WalletServiceMappers
{
    public static Domain.Entities.Users.Wallet.Wallet MapCreateWalletDto(this CreateWalletDto dto)
    {
        return new Domain.Entities.Users.Wallet.Wallet
        {
            UserId = dto.UserId,
            Balance = 0,
            TotalDeposited = 0,
            TotalWithdrawn = 0,
            TotalSpent = 0
        };
    }
    
    public static WalletDto MapToWalletDto(this Domain.Entities.Users.Wallet.Wallet wallet)
    {
        return new WalletDto
        {
            WalletId = wallet.Id,
            UserId = wallet.UserId,
            Balance = wallet.Balance,
            TotalDeposited = wallet.TotalDeposited,
            TotalWithdrawn = wallet.TotalWithdrawn,
            TotalSpent = wallet.TotalSpent
        };
    }
    
    
}