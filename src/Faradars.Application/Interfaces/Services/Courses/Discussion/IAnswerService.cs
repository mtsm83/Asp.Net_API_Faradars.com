using Faradars.Application.DTOs.Courses.Discussion.AnswerService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Discussion;

public interface IAnswerService
{
    Task<Result<AnswerDto>> AddAnswerAsync(AddAnswerDto dto, CancellationToken ct);
    Task<Result<AnswerDto>> UpdateAnswerAsync(UpdateAnswerDto dto, CancellationToken ct);
    Task<Result<AnswerDto>> DeleteAnswerAsync(int answerId, CancellationToken ct);
    Task<Result<List<AnswerDto>>> GetAllQuestionAnswersAsync(int questionId, CancellationToken ct);
}