using Faradars.Application.DTOs.Users.Wallet.WalletService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Wallet;
using Faradars.Application.Mappers.Users.Wallet;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Wallet;

public class WalletService(
    IRepository<User> userRepository,
    IRepository<Domain.Entities.Users.Wallet.Wallet> walletRepository,
    IUserContextService userContextService)
    : IWalletService, IScopedDependency
{
    public async Task<Result<WalletDto>> AddUserWalletAsync(CreateWalletDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<WalletDto>(Error.NotFound);
        var existingWallet = await walletRepository.TableNoTracking
            .FirstOrDefaultAsync(w => w.UserId == dto.UserId, ct);
        if (existingWallet != null)
            return Result.Failure<WalletDto>(Error.AlreadyExists);
        var wallet = dto.MapCreateWalletDto();
        wallet.CreatedBy = userContextService.CurrentUser.UserId;
        await walletRepository.AddAsync(wallet, ct);
        var walletDto = wallet.MapToWalletDto();

        return Result.Success(walletDto);
    }
    
    public async Task<Result<Unit>> DeleteUserWalletAsync(int walletId, CancellationToken ct)
    {
        var wallet = await walletRepository.GetByIdAsync(ct, walletId);
        if (wallet == null)
            return Result.Failure<Unit>(Error.NotFound);

        await walletRepository.DeleteAsync(wallet, ct);

        return Result.Success(Unit.Value);
    }
    
    public async Task<Result<WalletDto>> GetUserWalletAsync(int userId, CancellationToken ct)
    {
        var wallet = await walletRepository.TableNoTracking
            .FirstOrDefaultAsync(w => w.UserId == userId, ct);

        if (wallet == null)
            return Result.Failure<WalletDto>(Error.NotFound);

        var walletDto = wallet.MapToWalletDto();

        return Result.Success(walletDto);
    }
    
    public async Task<Result<List<WalletDto>>> GetAllUserWalletsAsync(CancellationToken ct)
    {
        var wallets = await walletRepository.TableNoTracking
            .ToListAsync(ct);

        if (wallets.Count == 0)
            return Result.Failure<List<WalletDto>>(Error.NotFound);

        var walletDtos = wallets.Select(w => w.MapToWalletDto()).ToList();

        return Result.Success(walletDtos);
    }
    
}