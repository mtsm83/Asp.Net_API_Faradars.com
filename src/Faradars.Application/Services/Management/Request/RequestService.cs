using Faradars.Application.DTOs.Management.Request.RequestService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.Application.Mappers.Management.Request;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Domain.Enums;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Management.Request;

public class RequestService(
    IRepository<UserRequest> requestRepository,
    IRepository<RequestSubject> subjectRepository,
    IUserContextService userContext)
    : IRequestService
{
    public async Task<Result<RequestDto>> CreateAsync(
        AddRequestDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var subjectExists = await subjectRepository
            .Table
            .AnyAsync(s => s.Id == dto.SubjectId, ct);

        if (!subjectExists)
            return Result.Failure<RequestDto>(Error.NotFound);

        var request = dto.MapAddRequest(currentUserId);

        await requestRepository.AddAsync(request, ct);

        return Result.Success(request.MapToRequestDto());
    }

    public async Task<Result<RequestDto>> UpdateAsync(
        UpdateRequestDto dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var request = await requestRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == dto.RequestId, ct);

        if (request is null)
            return Result.Failure<RequestDto>(Error.NotFound);

        if (request.UserId != currentUserId)
            return Result.Failure<RequestDto>(Error.Unauthorized);

        if (request.Status != RequestStatus.Pending)
            return Result.Failure<RequestDto>(Error.BadRequest);

        request.MapUpdateRequest(dto, currentUserId);

        return Result.Success(request.MapToRequestDto());
    }

    public async Task<Result<Unit>> DeleteAsync(
        int requestId,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var request = await requestRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == requestId, ct);

        if (request is null)
            return Result.Failure<Unit>(Error.NotFound);

        if (request.UserId != currentUserId)
            return Result.Failure<Unit>(Error.Unauthorized);

        if (request.Status != RequestStatus.Pending)
            return Result.Failure<Unit>(Error.BadRequest);

        await requestRepository.DeleteAsync(request, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<RequestDto>> GetByIdAsync(
        int requestId,
        CancellationToken ct)
    {
        var request = await requestRepository
            .Table
            .AsNoTracking()
            .Include(r => r.Subject)
            .FirstOrDefaultAsync(r => r.Id == requestId, ct);

        if (request is null)
            return Result.Failure<RequestDto>(Error.NotFound);

        return Result.Success(request.MapToRequestDto());
    }

    public async Task<Result<IReadOnlyList<RequestDto>>> GetAllAsync(
        CancellationToken ct)
    {
        var requests = await requestRepository
            .Table
            .AsNoTracking()
            .Include(r => r.Subject)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => r.MapToRequestDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<RequestDto>>(requests);
    }

    public async Task<Result<IReadOnlyList<RequestDto>>> GetByCurrentUserAsync(
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var requests = await requestRepository
            .Table
            .AsNoTracking()
            .Where(r => r.UserId == currentUserId)
            .Include(r => r.Subject)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => r.MapToRequestDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<RequestDto>>(requests);
    }
}