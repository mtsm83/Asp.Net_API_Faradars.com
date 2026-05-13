using Faradars.Application.DTOs.Management.Request.RequestMessageService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.Application.Mappers.Management.Request;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Management.Request;

public class RequestMessageService(
    IRepository<RequestMessage> messageRepository,
    IRepository<UserRequest> requestRepository,
    IUserContextService userContext)
    : IRequestMessageService, IScopedDependency
{
    public async Task<Result<RequestMessageDto>> AddAsync(
        AddRequestMessage dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var request = await requestRepository
            .Table
            .FirstOrDefaultAsync(r => r.Id == dto.RequestId, ct);

        if (request is null)
            return Result.Failure<RequestMessageDto>(Error.NotFound);

        if (request.UserId != currentUserId)
            return Result.Failure<RequestMessageDto>(Error.Unauthorized);

        var message = dto.MapAddRequestMessage(currentUserId);

        await messageRepository.AddAsync(message, ct);

        return Result.Success(message.MapToRequestMessageDto());
    }

    public async Task<Result<RequestMessageDto>> UpdateAsync(
        UpdateMessageRequest dto,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var message = await messageRepository
            .Table
            .FirstOrDefaultAsync(m => m.Id == dto.MessageId, ct);

        if (message is null)
            return Result.Failure<RequestMessageDto>(Error.NotFound);

        if (message.CreatedBy != currentUserId)
            return Result.Failure<RequestMessageDto>(Error.Unauthorized);

        message.MapUpdateRequestMessage(dto, currentUserId);

        return Result.Success(message.MapToRequestMessageDto());
    }

    public async Task<Result<Unit>> DeleteAsync(
        int messageId,
        CancellationToken ct)
    {
        var currentUserId = userContext.CurrentUser.UserId;

        var message = await messageRepository
            .Table
            .FirstOrDefaultAsync(m => m.Id == messageId, ct);

        if (message is null)
            return Result.Failure<Unit>(Error.NotFound);

        if (message.CreatedBy != currentUserId)
            return Result.Failure<Unit>(Error.Unauthorized);

        await messageRepository.DeleteAsync(message, ct);

        return Result.Success(Unit.Value);
    }

    public async Task<Result<RequestMessageDto>> GetByIdAsync(
        int messageId,
        CancellationToken ct)
    {
        var message = await messageRepository
            .Table
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == messageId, ct);

        if (message is null)
            return Result.Failure<RequestMessageDto>(Error.NotFound);

        return Result.Success(message.MapToRequestMessageDto());
    }

    public async Task<Result<IReadOnlyList<RequestMessageDto>>> GetByRequestIdAsync(
        int requestId,
        CancellationToken ct)
    {
        var messages = await messageRepository
            .Table
            .AsNoTracking()
            .Where(m => m.RequestId == requestId)
            .OrderBy(m => m.CreatedAt)
            .Select(m => m.MapToRequestMessageDto())
            .ToListAsync(ct);

        return Result.Success<IReadOnlyList<RequestMessageDto>>(messages);
    }
}