using Faradars.Application.DTOs.Courses.Content.SectionService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.Application.Mappers.Courses.Content;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Content;

public class SectionService(
    IUserContextService userContextService,
    IRepository<Section> sectionRepository
) : ISectionService
{
    public async Task<Result<SectionDto>> AddSectionAsync(AddSectionDto dto, CancellationToken ct)
    {
        var newSection = new Section();
        newSection.MapAddSectionDto(dto);
        newSection.CreatedBy = userContextService.CurrentUser.UserId;
        await sectionRepository.AddAsync(newSection, ct);
        var sectionDto = newSection.MapToSectionDto();
        return Result.Success(sectionDto);
    }

    public async Task<Result<SectionDto>> UpdateSectionAsync(UpdateSectionDto dto, CancellationToken ct)
    {
        var section = await sectionRepository.GetByIdAsync(ct, dto.SectionId);
        if (section == null)
            return Result.Failure<SectionDto>(Error.NotFound);

        section.MapUpdateSectionDto(dto);
        section.UpdatedBy = userContextService.CurrentUser.UserId;
        section.UpdatedAt = DateTime.Now;
        await sectionRepository.UpdateAsync(section, ct);
        var sectionDto = section.MapToSectionDto();
        return Result.Success(sectionDto);
    }

    public async Task<Result<SectionDto>> DeleteSectionAsync(int sectionId, CancellationToken ct)
    {
        // todo: what will happen to derivations? 
        throw new NotImplementedException();

        // var section = await sectionRepository
        //     .GetByIdAsync(ct, sectionId);
        //
        // if (section == null)
        //     return Result.Failure<Unit>(Error.NotFound);
        //
        // await sectionRepository.DeleteAsync(section, ct);
        // return Result.Success(Unit.Value);
    }

    public async Task<Result<List<SectionDto>>> GetCourseSectionsAsync(int courseId, CancellationToken ct)
    {
        var sections = await sectionRepository.TableNoTracking
            .Where(cs => cs.CourseId == courseId)
            .ToListAsync(ct);
        var sectionDtos = sections.Select(s => s.MapToSectionDto()).ToList();
        return Result.Success(sectionDtos);
    }
}