using Faradars.Application.DTOs.Users.Admin.AdminService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Users.Admin;

public interface IAdminService
{
    Task<Result<AdminDto>> AddAdminInfoAsync(AddAdminDto dto, CancellationToken ct);
    Task<Result<AdminDto>> UpdateAdminInfoAsync(UpdateAdminDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteAdminInfoAsync(int adminId, CancellationToken ct);
    Task<Result<AdminDto>> GetFullAdminInfoByIdAsync(int adminId, CancellationToken ct);
    Task<Result<List<AdminDto>>> GetAllAdminsInfoAsync(CancellationToken ct);

    Task<Result<AdminDismissalDto>> AddAdminDismissalAsync(AddAdminDismissalDto dto, CancellationToken ct);
    Task<Result<AdminDismissalDto>> GetAdminDismissalByIdAsync(int dismissalId, CancellationToken ct);
    Task<Result<List<AdminDismissalDto>>> GetAllAdminDismissalsAsync(CancellationToken ct);
}