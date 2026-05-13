using Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.Application.Mappers.Courses.Pricing;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Pricing.CoursePrice;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Pricing;

public class CoursePriceService(
    IRepository<Course> courseRepository,
    IRepository<CoursePrice> priceRepository,
    IUserContextService userContextService) : ICoursePriceService, IScopedDependency
{
    public async Task<Result<CoursePriceDto>> AddPriceToCourseAsync(AddPriceDto dto, CancellationToken ct)
    {
        var course = await courseRepository.GetByIdAsync(ct, dto.CourseId);
        if (course is null)
            return Result.Failure<CoursePriceDto>(Error.NotFound);
        var existingPrices = await priceRepository.TableNoTracking
            .Where(x => x.CourseId == dto.CourseId)
            .ToListAsync(ct);
        if (existingPrices.Any())
            return Result.Failure<CoursePriceDto>(Error.AlreadyExists);
        var newPrice = dto.MapAddPriceDto();
        newPrice.CreatedBy = userContextService.CurrentUser.UserId;

        await priceRepository.AddAsync(newPrice, ct);
        var priceDto = newPrice.MapToCoursePriceDto();
        return Result.Success(priceDto);
    }

    public async Task<Result<CoursePriceDto>> UpdatePriceOfCourseAsync(UpdatePriceDto dto, CancellationToken ct)
    {
        var price = await priceRepository.GetByIdAsync(ct, dto.PriceId);
        if (price is null)
            return Result.Failure<CoursePriceDto>(Error.NotFound);
        price.MapUpdatePriceDto(dto);
        price.UpdatedAt = DateTime.Now;
        price.UpdatedBy = userContextService.CurrentUser.UserId;
        await priceRepository.UpdateAsync(price, ct);
        var priceDto = price.MapToCoursePriceDto();
        return Result.Success(priceDto);
    }

    public async Task<Result<Unit>> DeleteCoursePriceAsync(int courseId, CancellationToken ct)
    {
        var price = await priceRepository.TableNoTracking
            .FirstOrDefaultAsync(p => p.CourseId == courseId, ct);
        if (price is null)
            return Result.Failure<Unit>(Error.NotFound);
        await priceRepository.DeleteAsync(price, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> DeleteCoursePriceByIdAsync(int coursePriceId, CancellationToken ct)
    {
        var price = await priceRepository.TableNoTracking
            .FirstOrDefaultAsync(p => p.Id == coursePriceId, ct);
        if (price is null)
            return Result.Failure<Unit>(Error.NotFound);
        await priceRepository.DeleteAsync(price, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<CoursePriceDto>> GetCoursePriceByIdAsync(int courseId, CancellationToken ct)
    {
        var price = await priceRepository.TableNoTracking
            .FirstOrDefaultAsync(p => p.CourseId == courseId, ct);
        if (price is null)
            return Result.Failure<CoursePriceDto>(Error.NotFound);
        var priceDto = price.MapToCoursePriceDto();
        return Result.Success(priceDto);
    }
}