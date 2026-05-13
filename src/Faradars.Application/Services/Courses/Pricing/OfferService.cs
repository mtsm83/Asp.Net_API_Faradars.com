using Faradars.Application.DTOs.Courses.Pricing.OfferService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.Application.Mappers.Courses.Pricing;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Pricing.Offer;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Pricing;

public class OfferService(
    IRepository<Offer> offerRepository,
    IRepository<Course> courseRepository,
    IRepository<OfferCourse> offerCourseRepository,
    IMediaService mediaService,
    IUserContextService userContextService) : IOfferService, IScopedDependency
{
    public async Task<Result<OfferDto>> AddOfferAsync(AddOfferDto dto, CancellationToken ct)
    {
        var newOffer = dto.MapAddOfferDto();
        if (dto.BannerImage != null)
        {
            var uploadResult = await mediaService.UploadAssetFileAsync(dto.BannerImage, ct);
            if (uploadResult.IsFailure)
                return Result.Failure<OfferDto>(uploadResult.Error);
            newOffer.BannerImageId = uploadResult.Value.Id;
        }

        newOffer.CreatedBy = userContextService.CurrentUser.UserId;
        await offerRepository.AddAsync(newOffer, ct);
        var offerDto = newOffer.MapToOfferDto();
        return Result.Success(offerDto);
    }

    public async Task<Result<OfferDto>> UpdateOfferAsync(UpdateOfferDto dto, CancellationToken ct)
    {
        var offer = await offerRepository.GetByIdAsync(ct, dto.OfferId);
        if (offer == null)
            return Result.Failure<OfferDto>(Error.NotFound);
        if (dto.BannerImage != null)
        {
            var uploadResult = await mediaService.UploadAssetFileAsync(dto.BannerImage, ct);
            if (uploadResult.IsFailure)
                return Result.Failure<OfferDto>(uploadResult.Error);
            offer.BannerImageId = uploadResult.Value.Id;
        }

        offer.MapUpdateOfferDto(dto);
        offer.UpdatedBy = userContextService.CurrentUser.UserId;
        offer.UpdatedAt = DateTime.Now;
        await offerRepository.UpdateAsync(offer, ct);
        var offerDto = offer.MapToOfferDto();
        return Result.Success(offerDto);
    }

    public async Task<Result<Unit>> DeleteOfferAsync(int offerId, CancellationToken ct)
    {
        var offer = await offerRepository.GetByIdAsync(ct, offerId);
        if (offer == null)
            return Result.Failure<Unit>(Error.NotFound);
        if (offer.BannerImageId != null)
        {
            var deleteResult = await mediaService.DeleteAssetFileAsync(offer.BannerImageId, ct);
            if (deleteResult.IsFailure)
                return Result.Failure<Unit>(deleteResult.Error);
        }

        await offerRepository.DeleteAsync(offer, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> AssignCourseToOfferAsync(int offerId, int courseId, CancellationToken ct)
    {
        var course = await courseRepository.GetByIdAsync(ct, courseId);
        if (course == null)
            return Result.Failure<Unit>(Error.NotFound);
        var offer = await offerRepository.GetByIdAsync(ct, offerId);
        if (offer == null)
            return Result.Failure<Unit>(Error.NotFound);
        var existingAssignment = await offerCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(oc => oc.CourseId == courseId && oc.OfferId == offerId, ct);
        if (existingAssignment != null)
            return Result.Failure<Unit>(Error.AlreadyExists);
        var newOfferCourse = new OfferCourse
        {
            CourseId = courseId,
            OfferId = offerId,
            CreatedBy = userContextService.CurrentUser.UserId
        };
        await offerCourseRepository.AddAsync(newOfferCourse, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> RemoveCourseThanOfferAsync(int offerId, int courseId, CancellationToken ct)
    {
        var assignment = await offerCourseRepository.TableNoTracking
            .FirstOrDefaultAsync(oc => oc.CourseId == courseId && oc.OfferId == offerId, ct);
        if (assignment == null)
            return Result.Failure<Unit>(Error.NotFound);
        await offerCourseRepository.DeleteAsync(assignment, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<OfferDto>> GetOfferByIdAsync(int offerId, CancellationToken ct)
    {
        var offer = await offerRepository.GetByIdAsync(ct, offerId);
        if (offer == null)
            return Result.Failure<OfferDto>(Error.NotFound);
        if (offer.BannerImageId != null)
        {
            var downloadResult = await mediaService.GetAssetFileByIdAsync(offer.BannerImageId, ct);
            if (downloadResult.IsFailure)
                return Result.Failure<OfferDto>(downloadResult.Error);
        }

        var offerDto = offer.MapToOfferDto();
        return Result.Success(offerDto);
    }

    public async Task<Result<List<OfferDto>>> GetAllOffersAsync(CancellationToken ct)
    {
        var offers = await  offerRepository.TableNoTracking.ToListAsync(ct);
        if (offers.Count == 0)
            return Result.Failure<List<OfferDto>>(Error.NotFound);
        var offerDtos = offers.Select(o => o.MapToOfferDto()).ToList();
        return Result.Success(offerDtos);
    }
}