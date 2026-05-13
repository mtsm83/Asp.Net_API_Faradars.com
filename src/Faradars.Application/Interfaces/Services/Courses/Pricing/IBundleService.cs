using Faradars.Application.DTOs.Courses.Pricing.BundleService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Pricing;

public interface IBundleService
{
    Task<Result<BundleDto>> AddBundleAsync(AddBundleDto dto, CancellationToken ct);
    Task<Result<BundleDto>> UpdateBundleAsync(UpdateBundleDto dto, CancellationToken ct);
    Task<Result<BundleDto>> DeleteBundleAsync(int bundleId, CancellationToken ct);
    Task<Result<Unit>> AddBannerImageToBundleAsync(int bundle, int bannerId, CancellationToken ct);
    Task<Result<Unit>> AssignCourseToBundleAsync(int bundle, int courseId, CancellationToken ct);
    Task<Result<Unit>> RemoveCourseThanBundleAsync(int bundle, int courseId, CancellationToken ct);
    Task<Result<BundleDto>> GetBundleByIdAsync(int bundle, CancellationToken ct);
    Task<Result<List<BundleDto>>> GetAllBundlesAsync(CancellationToken ct);
}