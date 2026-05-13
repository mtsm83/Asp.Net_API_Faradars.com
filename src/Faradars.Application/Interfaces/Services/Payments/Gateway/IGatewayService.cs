using Faradars.Application.DTOs.Payments.Gateway.GatewayService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Payments.Gateway;

public interface IGatewayService
{
    Task<Result<GatewayDto>> AddGatewayAsync(AddGatewayDto dto, CancellationToken ct);
    Task<Result<GatewayDto>> UpdateGatewayAsync(UpdateGatewayDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteGatewayAsync(int gatewayId, CancellationToken ct);
    Task<Result<GatewayDto>> GetGatewayByIdAsync(int gatewayId, CancellationToken ct);
    Task<Result<List<GatewayDto>>> GetAllGatewaysAsync(CancellationToken ct);
}