using Faradars.Application.DTOs.Courses.Content.CourseService;
using Faradars.Application.DTOs.Courses.Discussion.ReviewService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.Application.Interfaces.Services.Validation;
using Faradars.Application.Mappers.Courses.Content;
using Faradars.Application.Mappers.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Category;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Tag;
using Faradars.Domain.Entities.Users.Teacher;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Content;

public class CourseService(
    IRepository<Course> courseRepository,
    IRepository<Teacher> teacherRepository,
    IRepository<CourseCategory> courseCategoryRepository,
    IRepository<CourseReview> reviewRepository,
    IRepository<CourseTag> tagCategoryRepository,
    IFluentValidatorService validator,
    IUserContextService userContextService) : ICourseService, IScopedDependency
{
    public async Task<Result<PreviewCourseDto>> AddCourseAsync(AddCourseDto dto, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);
        if (validationResult.IsFailure)
            return Result.Failure<PreviewCourseDto>(validationResult.Error);
        
        var newCourse = new Course();
        newCourse.MapAddDtoToCourse(dto);
        newCourse.CreatedBy = userContextService.CurrentUser.UserId;

        await courseRepository.AddAsync(newCourse, ct);
        var courseDto = new PreviewCourseDto();
        newCourse.MapToPreviewCourseDto(courseDto);
        return Result.Success(courseDto);
    }

    public async Task<Result<PreviewCourseDto>> UpdateCourseAsync(UpdateCourseDto dto, CancellationToken ct)
    {
        var course = await courseRepository.TableNoTracking
            .FirstOrDefaultAsync(c => c.Id == dto.CourseId, ct);
        if (course == null)
            return Result.Failure<PreviewCourseDto>(Error.NotFound);
        course.MapUpdateDtoToCourse(dto);
        await courseRepository.UpdateAsync(course, ct);
        var courseDto = new PreviewCourseDto();
        course.MapToPreviewCourseDto(courseDto);
        return Result.Success(courseDto);
    }

    public async Task<Result<PreviewCourseDto>> UpdateCourseAverageRatingAsync(int courseId, CancellationToken ct)
    {
        var course = await courseRepository.TableNoTracking
            .FirstOrDefaultAsync(c => c.Id == courseId, ct);
        if (course == null)
            return Result.Failure<PreviewCourseDto>(Error.NotFound);
        // var relatedReviewsResult = await reviewService.GetCourseReviewsAsync(courseId, ct);
        var reviews = await reviewRepository.TableNoTracking
            .Where(r => r.CourseId == courseId).ToListAsync(ct);
        if (reviews.Count < 1)
            return Result.Failure<PreviewCourseDto>(Error.NotFound);
        var relatedReviews = reviews.Select(r => r.MapToReviewDto()).ToList();
        course.AverageRating = relatedReviews.Average(r => r.Rating);
        await courseRepository.UpdateAsync(course, ct);
        var courseDto =  new PreviewCourseDto();
        course.MapToPreviewCourseDto(courseDto);
        return Result.Success(courseDto);
    }

    public async Task<Result<PreviewCourseDto>> DeleteCourseAsync(int courseId, CancellationToken ct)
    {
        // var course = await courseRepository.Table
        //     .Where(c => c.Id == courseId)
        //     .Include(c => c.Sections)
        //     .ThenInclude(s => s.Lessons)
        //     .ThenInclude(s => s.LessonAssets)
        //     .Include(c => c.CourseCategories)
        //     .FirstOrDefaultAsync(ct);
        //
        // if (course == null)
        //     return Result.Failure<PreviewCourseDto>(Error.NotFound);
        //
        // if (course.Sections.Count > 0)
        // {
        //     foreach (var section in course.Sections)
        //     {
        //        
        //         if (section.Lessons.Count > 0)
        //         {
        //             foreach (var lesson in section.Lessons)
        //             {
        //                 foreach (var asset in lesson)
        //                     await assetRepository.DeleteAsync(asset, ct);
        //                 await lessonRepository.DeleteAsync(lesson, ct);
        //             }
        //         }
        //
        //         await sectionRepository.DeleteAsync(section, ct);
        //     }
        // }
        //
        // if (course.CourseCategories != null)
        // {
        //     foreach (var cc in course.CourseCategories)
        //         await courseCategoryRepository.DeleteAsync(cc, ct);
        // }
        //
        // await courseRepository.DeleteAsync(course, ct);
        // await courseRepository.SaveChangesAsync(ct);
        //
        return Result.Success(new PreviewCourseDto());
    }


    public async Task<Result<List<PreviewCourseDto>>> GetAllCoursesAsync(CancellationToken ct)
    {
        var courses = await courseRepository
            .TableNoTracking
            .ToListAsync(ct);

        if (courses.Count < 1)
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetAllSearchedCoursesAsync(string searchText,
        CancellationToken ct)
    {
        var courses = await courseRepository.TableNoTracking
            .Where(c => c.Title.Contains(searchText))
            .ToListAsync(ct);

        if (courses.Count < 1)
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<PreviewCourseDto>> GetCourseByIdAsync(int courseId, CancellationToken ct)
    {
        var course = await courseRepository.TableNoTracking
            .FirstOrDefaultAsync(c => c.Id == courseId, ct);

        if (course == null)
            return Result.Failure<PreviewCourseDto>(Error.NotFound);

        var courseDto = new PreviewCourseDto();
        course.MapToPreviewCourseDto(courseDto);

        return Result.Success(courseDto);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetCoursesByCategoryAsync(int categoryId, CancellationToken ct)
    {
        var courseIds = await courseCategoryRepository.TableNoTracking
            .Where(cc => cc.CategoryId == categoryId)
            .Select(cc => cc.CourseId)
            .ToListAsync(ct);

        if (!courseIds.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courses = await courseRepository.TableNoTracking
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetCoursesByTagAsync(int tagId, CancellationToken ct)
    {
        var courseIds = await tagCategoryRepository.TableNoTracking
            .Where(cc => cc.TagId == tagId)
            .Select(cc => cc.CourseId)
            .ToListAsync(ct);

        if (!courseIds.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courses = await courseRepository.TableNoTracking
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetAllOfferCoursesId(int offerId, CancellationToken ct)
    {
        var courses = await courseRepository.TableNoTracking
            .Include(c => c.OfferCourses)
            .Where(c => c.OfferCourses.Any(o => o.Id == offerId))
            .ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetAllBundleCoursesAsync(int bundleId, CancellationToken ct)
    {
        var courses = await courseRepository.TableNoTracking
            .Include(c => c.BundleItems)
            .Where(c => c.BundleItems.Any(o => o.BundleId == bundleId))
            .ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetAllSubscriptionCoursesAsync(int subscriptionId,
        CancellationToken ct)
    {
        var courses = await courseRepository.TableNoTracking
            .Include(c => c.SubscriptionCourses)
            .Where(c => c.SubscriptionCourses.Any(o => o.PlanId == subscriptionId))
            .ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }

    public async Task<Result<List<PreviewCourseDto>>> GetAllTeacherCoursesAsync(int teacherId, CancellationToken ct)
    {
        var user = await teacherRepository.GetByIdAsync(ct, teacherId);
        if (user is null)
            return Result.Failure<List<PreviewCourseDto>>(Error.UserNotFound);

        var courses = await courseRepository.TableNoTracking
            .Where(c => c.TeacherId == user.Id).ToListAsync(ct);

        if (!courses.Any())
            return Result.Failure<List<PreviewCourseDto>>(Error.NotFound);

        var courseDtos = courses.Select(c =>
        {
            var dto = new PreviewCourseDto();
            c.MapToPreviewCourseDto(dto);
            return dto;
        }).ToList();

        return Result.Success(courseDtos);
    }
}