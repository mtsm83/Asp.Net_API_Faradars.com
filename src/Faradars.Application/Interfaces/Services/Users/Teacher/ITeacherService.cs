using Faradars.Application.DTOs.Users.Teacher.TeacherService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Users.Teacher;

public interface ITeacherService
{
    Task<Result<TeacherDto>> AddTeacherInfoAsync(AddTeacherDto dto, CancellationToken ct);
    Task<Result<TeacherDto>> UpdateTeacherInfoAsync(UpdateTeacherDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteTeacherInfoAsync(int teacherId, CancellationToken ct);
    Task<Result<TeacherDto>> GetFullTeacherInfoByIdAsync(int teacherId, CancellationToken ct);
    Task<Result<TeacherDto>> GetFullTeacherInfoByCourseIdAsync(int courseId, CancellationToken ct);
    Task<Result<List<TeacherDto>>> GetAllTeachersInfoAsync(CancellationToken ct);

    Task<Result<TeacherDismissalDto>> AddTeacherDismissalAsync(DismissTeacherDto dto, CancellationToken ct);
    Task<Result<Unit>> DeleteTeacherDismissalAsync(int dismissalId, CancellationToken ct);
    Task<Result<TeacherDismissalDto>> GetTeacherDismissalByIdAsync(int dismissalId, CancellationToken ct);
    Task<Result<List<TeacherDismissalDto>>> GetAllTeacherDismissalsAsync(CancellationToken ct);
    
}