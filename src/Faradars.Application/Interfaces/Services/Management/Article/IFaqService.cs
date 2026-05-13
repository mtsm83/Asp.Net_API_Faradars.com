using Faradars.Application.DTOs.Management.Article.FaqService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Management.Article;

public interface IFaqService
{
    Task<Result<FaqDto>> AddFaqAsync(AddFaqDto dto, CancellationToken ct);
    Task<Result<FaqDto>> UpdateFaqAsync(UpdateFaqDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteFaqAsync(int faqId, CancellationToken ct);
    Task<Result<FaqDto>> GetFaqByIdAsync(int faqId, CancellationToken ct);
    Task<Result<List<FaqDto>>> GetAllFaqsAsync(CancellationToken ct);
}