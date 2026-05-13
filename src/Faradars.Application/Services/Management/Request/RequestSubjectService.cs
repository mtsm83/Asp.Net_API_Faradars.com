using Faradars.Application.DTOs.Management.Request.RequestSubjectService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.Application.Mappers.Management.Request;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Management.Request;

public class RequestSubjectService(
    IRepository<RequestSubject> subjectRepository,
    IUserContextService userContext)
    : IRequestSubjectService, IScopedDependency
{
    public async Task<Result<SubjectDto>> CreateAsync(
        AddRequestSubjectDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var subject = dto.MapAddRequestSubject(currentUserId);

        await subjectRepository.AddAsync(subject, ct);

        return Result.Success(subject.MapToSubjectDto());
    }

    public async Task<Result<SubjectDto>> UpdateAsync(
        UpdateRequestSubjectDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var subject = await subjectRepository
            .Table
            .FirstOrDefaultAsync(s => s.Id == dto.SubjectId, ct);

        if (subject is null)
            return Result.Failure<SubjectDto>(Error.NotFound);

        subject.MapUpdateRequestSubject(dto, currentUserId);

        return Result.Success(subject.MapToSubjectDto());
    }

    public async Task<Result<Unit>> DeleteAsync(
        int subjectId,
        CancellationToken ct)
    {
        var subject = await subjectRepository
            .Table
            .FirstOrDefaultAsync(s => s.Id == subjectId, ct);

        if (subject is null)
            return Result.Failure<Unit>(Error.NotFound);

        await subjectRepository.DeleteAsync(subject, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<SubjectDto>> GetByIdAsync(
        int subjectId,
        CancellationToken ct)
    {
        var subject = await subjectRepository
            .Table
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == subjectId, ct);

        if (subject is null)
            return Result.Failure<SubjectDto>(Error.NotFound);

        return Result.Success(subject.MapToSubjectDto());
    }

    public async Task<Result<IReadOnlyList<SubjectDto>>> GetAllAsync(
        CancellationToken ct)
    {
        var subjects = await subjectRepository
            .Table
            .AsNoTracking()
            .OrderBy(s => s.Title)
            .Select(s => s.MapToSubjectDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<SubjectDto>>(subjects);
    }
}