using Faradars.Application.DTOs.Management.Request.RequestMessageService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Management.Request;

public interface IRequestMessageService
{
    Task<Result<RequestMessageDto>> AddAsync(AddRequestMessage dto, CancellationToken ct);
    Task<Result<RequestMessageDto>> UpdateAsync(UpdateMessageRequest dto, CancellationToken ct);
    Task<Result<Unit>> DeleteAsync(int messageId, CancellationToken ct);
    Task<Result<RequestMessageDto>> GetByIdAsync(int messageId, CancellationToken ct);
    Task<Result<IReadOnlyList<RequestMessageDto>>> 
        GetByRequestIdAsync(int requestId, CancellationToken ct);
}