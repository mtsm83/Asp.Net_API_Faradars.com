using Faradars.Application.DTOs.Courses.Pricing.OfferService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Pricing;

public interface IOfferService
{
    Task<Result<OfferDto>> AddOfferAsync(AddOfferDto dto, CancellationToken ct);
    Task<Result<OfferDto>> UpdateOfferAsync(UpdateOfferDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteOfferAsync(int offerId, CancellationToken ct);
    Task<Result<Unit>> AssignCourseToOfferAsync(int offerId, int courseId, CancellationToken ct);
    Task<Result<Unit>> RemoveCourseThanOfferAsync(int offerId, int courseId, CancellationToken ct);
    Task<Result<OfferDto>> GetOfferByIdAsync(int offerId, CancellationToken ct);
    Task<Result<List<OfferDto>>> GetAllOffersAsync(CancellationToken ct);
}