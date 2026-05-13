using Faradars.Application.DTOs.Courses.Pricing.BundleService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.Application.Mappers.Courses.Pricing;
using Faradars.Domain.Entities.Courses.Pricing.Bundle;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Pricing;

public class BundleService(
    IRepository<Bundle> bundleRepository,
    IRepository<BundleCourse> bundleCourseRepository,
    IUserContextService userContextService) : IBundleService, IScopedDependency
{
    public async Task<Result<BundleDto>> AddBundleAsync(AddBundleDto dto, CancellationToken ct)
    {
        
        var newBundle = dto.MapAddDtoToBundle();
        newBundle.CreatedBy = userContextService.CurrentUser.UserId;
        newBundle.CreatedAt = DateTime.UtcNow;

        await bundleRepository.AddAsync(newBundle, ct);
        var bundleDto = newBundle.MapToBundleDto();
        return Result.Success(bundleDto);
    }

    public async Task<Result<BundleDto>> UpdateBundleAsync(UpdateBundleDto dto, CancellationToken ct)
    {
        var bundle = await bundleRepository.TableNoTracking
            .FirstOrDefaultAsync(b => b.Id == dto.BundleId, ct);
        
        if (bundle == null)
            return Result.Failure<BundleDto>(Error.NotFound);

        bundle.MapUpdateDtoToBundle(dto);
        bundle.UpdatedBy = userContextService.CurrentUser.UserId;
        bundle.UpdatedAt = DateTime.UtcNow;

        await bundleRepository.UpdateAsync(bundle, ct);

        var bundleDto = bundle.MapToBundleDto();
        return Result.Success(bundleDto);
    }

    public async Task<Result<BundleDto>> DeleteBundleAsync(int bundleId, CancellationToken ct)
    {
        var bundle = await bundleRepository.TableNoTracking
            .Include(b => b.BundleItems)
            .FirstOrDefaultAsync(b => b.Id == bundleId, ct);

        if (bundle == null)
            return Result.Failure<BundleDto>(Error.NotFound);

        if (bundle.BundleItems.Any())
        {
            bundle.IsDeleted = true;
            bundle.DeletedAt = DateTime.UtcNow;
            bundle.DeletedBy = userContextService.CurrentUser.UserId;
            await bundleRepository.UpdateAsync(bundle, ct);
        }
        else
        {
            await bundleRepository.DeleteAsync(bundle, ct);
        }

        var bundleDto = bundle.MapToBundleDto();
        return Result.Success(bundleDto);
    }

    public async Task<Result<Unit>> AddBannerImageToBundleAsync(int bundleId, int bannerId, CancellationToken ct)
    {
        var bundle = await bundleRepository.GetByIdAsync(ct, bundleId);
        
        if (bundle == null)
            return Result.Failure<Unit>(Error.NotFound);

        bundle.BannerImageId = bannerId;
        bundle.UpdatedBy = userContextService.CurrentUser.UserId;
        bundle.UpdatedAt = DateTime.UtcNow;

        await bundleRepository.UpdateAsync(bundle, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> AssignCourseToBundleAsync(int bundleId, int courseId, CancellationToken ct)
    {
        var bundle = await bundleRepository.GetByIdAsync(ct, bundleId);
        if (bundle == null)
            return Result.Failure<Unit>(Error.NotFound);

        var existingAssignment = await bundleCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(bc => bc.BundleId == bundleId && bc.CourseId == courseId, ct);
        
        if (existingAssignment != null)
            return Result.Failure<Unit>(Error.AlreadyExists);

        var bundleCourse = new BundleCourse
        {
            BundleId = bundleId,
            CourseId = courseId,
            CreatedAt = DateTime.UtcNow
        };

        await bundleCourseRepository.AddAsync(bundleCourse, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> RemoveCourseThanBundleAsync(int bundleId, int courseId, CancellationToken ct)
    {
        var bundleCourse = await bundleCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(bc => bc.BundleId == bundleId && bc.CourseId == courseId, ct);

        if (bundleCourse == null)
            return Result.Failure<Unit>(Error.NotFound);

        await bundleCourseRepository.DeleteAsync(bundleCourse, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<BundleDto>> GetBundleByIdAsync(int bundleId, CancellationToken ct)
    {
        var bundle = await bundleRepository.TableNoTracking
            .Include(b => b.BundleItems)
            .ThenInclude(bc => bc.Course)
            .FirstOrDefaultAsync(b => b.Id == bundleId && !b.IsDeleted, ct);

        if (bundle == null)
            return Result.Failure<BundleDto>(Error.NotFound);

        var bundleDto = bundle.MapToBundleDto();
        return Result.Success(bundleDto);
    }

    public async Task<Result<List<BundleDto>>> GetAllBundlesAsync(CancellationToken ct)
    {
        var bundles = await bundleRepository.TableNoTracking
            .Include(b => b.BundleItems)
            .Where(b => !b.IsDeleted)
            .ToListAsync(ct);

        if (!bundles.Any())
            return Result.Failure<List<BundleDto>>(Error.NotFound);

        var bundleDtos = bundles.Select(b =>
        {
            var dto = new BundleDto();
            b.MapToBundleDto();
            return dto;
        }).ToList();

        return Result.Success(bundleDtos);
    }
}