using Faradars.Application.DTOs.Courses.Content.SectionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Content;

public interface ISectionService
{
    Task<Result<SectionDto>> AddSectionAsync(AddSectionDto dto, CancellationToken ct);
    Task<Result<SectionDto>> UpdateSectionAsync(UpdateSectionDto dto, CancellationToken ct);
    Task<Result<SectionDto>> DeleteSectionAsync(int sectionId, CancellationToken ct);
    Task<Result<List<SectionDto>>> GetCourseSectionsAsync(int courseId, CancellationToken ct);
}