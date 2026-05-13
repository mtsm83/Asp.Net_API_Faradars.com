using Faradars.Application.DTOs.Users.Wallet.WalletService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Users.Wallet;

public interface IWalletService
{
    Task<Result<WalletDto>> AddUserWalletAsync(CreateWalletDto dto, CancellationToken ct); // must be called in user creation
    Task<Result<Unit>> DeleteUserWalletAsync(int walletId, CancellationToken ct); // must be called in user deletion
    Task<Result<WalletDto>> GetUserWalletAsync(int userId, CancellationToken ct);
    Task<Result<List<WalletDto>>> GetAllUserWalletsAsync(CancellationToken ct);
}