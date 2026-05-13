using Faradars.Application.DTOs.Users.Admin.AdminService;
using Faradars.Domain.Entities.Users.Admin;

namespace Faradars.Application.Mappers.Users.Admin;

public static class AdminServiceMappers
{
    public static void MapAddAdminDto(this Domain.Entities.Users.Admin.Admin admin, AddAdminDto dto)
    {
        admin.UserId = dto.UserId;
        admin.Description = dto.Description ?? admin.Description;
        admin.Bio = dto.Bio ?? admin.Bio;
        admin.LinkedinUrl = dto.LinkedinUrl ?? admin.LinkedinUrl;
    }

    public static void MapUpdateAdminDto(this Domain.Entities.Users.Admin.Admin admin, UpdateAdminDto dto)
    {
        admin.Description = dto.Description ?? admin.Description;
        admin.Bio = dto.Bio ?? admin.Bio;
        admin.LinkedinUrl = dto.LinkedinUrl ?? admin.LinkedinUrl;
    }

    public static void MapAdminDismissalDto(this AdminDismissal adminDismissal, AddAdminDismissalDto dismissalDto)
    {
        adminDismissal.AdminId = dismissalDto.AdminId;
        adminDismissal.DismissalReason = dismissalDto.DismissalReason;
        adminDismissal.DismissalTypeId = dismissalDto.DismissalTypeId;
    }
    public static AdminDismissalDto MapToAdminDismissalDto(this AdminDismissal adminDismissal)
    {
        return new AdminDismissalDto
        {
            DismissalId = adminDismissal.Id,
            AdminId = adminDismissal.AdminId,
            DismissalReason = adminDismissal.DismissalReason,
            DismissalTypeId = adminDismissal.DismissalTypeId
        };
    }

    public static AdminDto MapToAdminDto(this Domain.Entities.Users.Admin.Admin admin)
    {
        return new AdminDto
        {
            AdminId = admin.Id,
            PromoterId = admin.CreatedBy,
            PromotedAt = admin.CreatedAt,
            Description = admin.Description,
            Bio = admin.Bio,
            LinkedinUrl = admin.LinkedinUrl,
        };
    }
}