using Faradars.Application.DTOs.Courses.Discussion.QuestionService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Discussion;

public interface IQuestionService
{
    Task<Result<QuestionDto>> AddQuestionAsync(AddQuestionDto dto, CancellationToken ct);
    Task<Result<QuestionDto>> UpdateQuestionAsync(UpdateQuestionDto dto, CancellationToken ct);
    Task<Result<QuestionDto>> DeleteQuestionAsync(int questionId, CancellationToken ct);
    Task<Result<QuestionDto>> GetQuestionByIdAsync(int questionId, CancellationToken ct);
    Task<Result<List<QuestionDto>>> GetCourseQuestionsAsync(int courseId, CancellationToken ct);
}