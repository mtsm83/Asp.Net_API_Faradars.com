using Faradars.Application.DTOs.Users.Teacher.TeacherService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Teacher;
using Faradars.Application.Mappers.Users.Teacher;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Entities.Users.Teacher;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Teacher;

public class TeacherService(
    IRepository<User> userRepository,
    IRepository<Course> courseRepository,
    IRepository<TeacherDismissal> teachDismissRepository,
    IRepository<DismissalType> dismissTypeRepository,
    IRepository<Domain.Entities.Users.Teacher.Teacher> teacherRepository,
    IRepository<TeacherDismissal> teacherDismissalRepository,
    IUserContextService userContextService) : ITeacherService, IScopedDependency
{
    public async Task<Result<TeacherDto>> AddTeacherInfoAsync(AddTeacherDto dto, CancellationToken ct)
    {
        // Note: must be a normal user at first to be added as a teacher
        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        var newTeacher = dto.MapAddTeacherDto();
        newTeacher.CreatedBy = userContextService.CurrentUser.UserId;
        await teacherRepository.AddAsync(newTeacher, ct);
        var teacherDto = newTeacher.MapToTeacherDto();
        return Result.Success(teacherDto);
    }

    public async Task<Result<TeacherDto>> UpdateTeacherInfoAsync(UpdateTeacherDto dto, CancellationToken ct)
    {
        var teacher = await teacherRepository.GetByIdAsync(ct, dto.TeacherId);
        if (teacher == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, teacher.UserId);
        if (user == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        teacher.MapUpdateTeacherDto(dto);
        teacher.UpdatedBy = userContextService.CurrentUser.UserId;
        teacher.UpdatedAt = DateTime.Now;
        await teacherRepository.UpdateAsync(teacher, ct);
        var teacherDto = teacher.MapToTeacherDto();
        return Result.Success(teacherDto);
    }

    public async Task<Result<Unit>> DeleteTeacherInfoAsync(int teacherId, CancellationToken ct)
    {
        var teacher = await teacherRepository.GetByIdAsync(ct, teacherId);
        if (teacher == null)
            return Result.Failure<Unit>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, teacher.UserId);
        if (user == null)
            return Result.Failure<Unit>(Error.NotFound);

        await teacherRepository.DeleteAsync(teacher, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<TeacherDismissalDto>> AddTeacherDismissalAsync(DismissTeacherDto dto, CancellationToken ct)
    {
        var teacher = await teacherRepository.GetByIdAsync(ct, dto.TeacherId);
        if (teacher == null)
            return Result.Failure<TeacherDismissalDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, teacher.UserId);
        if (user == null)
            return Result.Failure<TeacherDismissalDto>(Error.NotFound);

        var dismissalType = await dismissTypeRepository.GetByIdAsync(ct, dto.DismissalTypeId);
        if (dismissalType == null)
            return Result.Failure<TeacherDismissalDto>(Error.NotFound);

        var newTeacherDismissal = new TeacherDismissal();
        newTeacherDismissal.MapAddTeacherDismissalDto(dto);
        newTeacherDismissal.CreatedBy = userContextService.CurrentUser.UserId;
        await teachDismissRepository.AddAsync(newTeacherDismissal, ct);
        var dismissalDto = newTeacherDismissal.MapToTeacherDismissalDto();
        return Result.Success(dismissalDto);
    }

    public async Task<Result<Unit>> DeleteTeacherDismissalAsync(int dismissalId, CancellationToken ct)
    {
        var dismissal = await teacherDismissalRepository.GetByIdAsync(ct, dismissalId);
        if (dismissal == null)
            return Result.Failure<Unit>(Error.NotFound);
        if (dismissal.CreatedBy != userContextService.CurrentUser.UserId ||
            !userContextService.CurrentUser.IsAdmin)
            return Result.Failure<Unit>(Error.Unauthorized);
        await teacherDismissalRepository.DeleteAsync(dismissal, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<TeacherDismissalDto>> GetTeacherDismissalByIdAsync(int dismissalId, CancellationToken ct)
    {
        var dismissal = await teacherDismissalRepository.GetByIdAsync(ct, dismissalId);
        if (dismissal == null)
            return Result.Failure<TeacherDismissalDto>(Error.NotFound);
        var dismissalDto = dismissal.MapToTeacherDismissalDto();
        return Result.Success(dismissalDto);
    }

    public async Task<Result<List<TeacherDismissalDto>>> GetAllTeacherDismissalsAsync(CancellationToken ct)
    {
        var dismissals = await  teacherDismissalRepository
            .TableNoTracking.AsNoTracking().ToListAsync(ct);
        if  (dismissals.Count == 0)
            return Result.Failure<List<TeacherDismissalDto>>(Error.NotFound);
        var dismissalDtos = dismissals.Select(d => d.MapToTeacherDismissalDto()).ToList();
        return Result.Success(dismissalDtos);
    }

    public async Task<Result<TeacherDto>> GetFullTeacherInfoByIdAsync(int teacherId, CancellationToken ct)
    {
        var teacher = await teacherRepository.GetByIdAsync(ct, teacherId);
        if (teacher == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, teacher.UserId);
        if (user == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        var teacherDto = teacher.MapToTeacherDto();
        return Result.Success(teacherDto);
    }

    public async Task<Result<TeacherDto>> GetFullTeacherInfoByCourseIdAsync(int courseId, CancellationToken ct)
    {
        var teacher = await courseRepository.TableNoTracking
            .Where(c => c.Id == courseId).Include(c => c.Teacher)
            .Select(c => c.Teacher).FirstOrDefaultAsync(ct);
        if (teacher == null)
            return Result.Failure<TeacherDto>(Error.NotFound);

        var teacherDto = teacher.MapToTeacherDto();
        return Result.Success(teacherDto);
    }

    public async Task<Result<List<TeacherDto>>> GetAllTeachersInfoAsync(CancellationToken ct)
    {
        var teachers = await teacherRepository.TableNoTracking
            .ToListAsync(ct);
        if (teachers.Count == 0)
            return Result.Failure<List<TeacherDto>>(Error.NotFound);

        var teacherDtos = teachers.Select(teacher => teacher.MapToTeacherDto()).ToList();
        return Result.Success(teacherDtos);
    }

}