using Faradars.Application.DTOs.Users.Admin.AdminService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Admin;
using Faradars.Application.Mappers.Users.Admin;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Domain.Entities.Users.Admin;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Admin;

public class AdminService(
    IRepository<User> userRepository,
    IRepository<Course> courseRepository,
    IRepository<AdminDismissal> adminDismissRepository,
    IRepository<DismissalType> dismissTypeRepository,
    IRepository<Domain.Entities.Users.Admin.Admin> adminRepository,
    IUserContextService userContextService) : IAdminService, IScopedDependency
{
    public async Task<Result<AdminDto>> AddAdminInfoAsync(AddAdminDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<AdminDto>(Error.NotFound);

        var newAdmin = new Domain.Entities.Users.Admin.Admin();
        newAdmin.MapAddAdminDto(dto);
        newAdmin.CreatedBy = userContextService.CurrentUser.UserId; // Promoter
        await adminRepository.AddAsync(newAdmin, ct);
        var adminDto = newAdmin.MapToAdminDto();
        return Result.Success(adminDto);
    }

    public async Task<Result<AdminDto>> UpdateAdminInfoAsync(UpdateAdminDto dto, CancellationToken ct)
    {
        var admin = await adminRepository.GetByIdAsync(ct, dto.AdminId);
        if (admin == null)
            return Result.Failure<AdminDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, admin.UserId);
        if (user == null)
            return Result.Failure<AdminDto>(Error.NotFound);

        admin.MapUpdateAdminDto(dto);
        admin.UpdatedBy = userContextService.CurrentUser.UserId;
        admin.UpdatedAt = DateTime.Now;
        await adminRepository.UpdateAsync(admin, ct);
        var adminDto = admin.MapToAdminDto();
        return Result.Success(adminDto);
    }

    public async Task<Result<Unit>> DeleteAdminInfoAsync(int adminId, CancellationToken ct)
    {
        var admin = await adminRepository.GetByIdAsync(ct, adminId);
        if (admin == null)
            return Result.Failure<Unit>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, admin.UserId);
        if (user == null)
            return Result.Failure<Unit>(Error.NotFound);

        await adminRepository.DeleteAsync(admin, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<AdminDismissalDto>> AddAdminDismissalAsync(AddAdminDismissalDto dto, CancellationToken ct)
    {
        var admin = await adminRepository.GetByIdAsync(ct, dto.AdminId);
        if (admin == null)
            return Result.Failure<AdminDismissalDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, admin.UserId);
        if (user == null)
            return Result.Failure<AdminDismissalDto>(Error.NotFound);

        var dismissalType = await dismissTypeRepository.GetByIdAsync(ct, dto.DismissalTypeId);
        if (dismissalType == null)
            return Result.Failure<AdminDismissalDto>(Error.NotFound);

        var newAdminDismissal = new AdminDismissal();
        newAdminDismissal.MapAdminDismissalDto(dto);
        newAdminDismissal.CreatedBy = userContextService.CurrentUser.UserId;
        await adminDismissRepository.AddAsync(newAdminDismissal, ct);
        var dismissalDto = newAdminDismissal.MapToAdminDismissalDto();
        return Result.Success(dismissalDto);
    }

    public async Task<Result<AdminDismissalDto>> GetAdminDismissalByIdAsync(int dismissalId, CancellationToken ct)
    {
        var dismissal = await adminDismissRepository.GetByIdAsync(ct, dismissalId);
        if (dismissal == null)
            return Result.Failure<AdminDismissalDto>(Error.NotFound);
        var dismissalDto = dismissal.MapToAdminDismissalDto();
        return Result.Success(dismissalDto);
    }

    public async Task<Result<List<AdminDismissalDto>>> GetAllAdminDismissalsAsync(CancellationToken ct)
    {
        var dismissals = await adminDismissRepository
            .TableNoTracking.AsNoTracking().ToListAsync(ct);
        if  (dismissals.Count == 0)
            return Result.Failure<List<AdminDismissalDto>>(Error.NotFound);
        var dismissalDtos = dismissals.Select(d => d.MapToAdminDismissalDto()).ToList();
        return Result.Success(dismissalDtos);
    }

    public async Task<Result<AdminDto>> GetFullAdminInfoByIdAsync(int adminId, CancellationToken ct)
    {
        var admin = await adminRepository.GetByIdAsync(ct, adminId);
        if (admin == null)
            return Result.Failure<AdminDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, admin.UserId);
        if (user == null)
            return Result.Failure<AdminDto>(Error.NotFound);

        var adminDto = admin.MapToAdminDto();
        return Result.Success(adminDto);
    }

    public async Task<Result<List<AdminDto>>> GetAllAdminsInfoAsync(CancellationToken ct)
    {
        var admins = await adminRepository.TableNoTracking
            .ToListAsync(ct);
        if (admins.Count == 0)
            return Result.Failure<List<AdminDto>>(Error.NotFound);

        var adminDtos = admins.Select(admin => admin.MapToAdminDto()).ToList();
        return Result.Success(adminDtos);
    }
}