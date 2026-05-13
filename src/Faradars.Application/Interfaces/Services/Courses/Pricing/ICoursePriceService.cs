using Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Pricing;

public interface ICoursePriceService
{
    Task<Result<CoursePriceDto>> AddPriceToCourseAsync(AddPriceDto dto, CancellationToken ct);
    Task<Result<CoursePriceDto>> UpdatePriceOfCourseAsync(UpdatePriceDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteCoursePriceAsync(int courseId, CancellationToken ct);
    Task<Result<Unit>> DeleteCoursePriceByIdAsync(int coursePriceId, CancellationToken ct);
    Task<Result<CoursePriceDto>> GetCoursePriceByIdAsync(int courseId, CancellationToken ct);
}