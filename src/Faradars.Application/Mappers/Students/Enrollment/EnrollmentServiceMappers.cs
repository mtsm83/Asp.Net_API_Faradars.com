using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Domain.Entities.Students.Enrollment;

namespace Faradars.Application.Mappers.Students.Enrollment;

public static class EnrollmentServiceMappers
{
    public static Domain.Entities.Students.Enrollment.Enrollment MapAddToEnrollment(this EnrollUserDto dto)
    {
        return new Domain.Entities.Students.Enrollment.Enrollment
        {
            CourseId = dto.CourseId,
            StudentId = dto.UserId,
            PaymentId = dto.PaymentId,
        };
    }
    public static EnrollmentDto MapToEnrollmentDto(this Domain.Entities.Students.Enrollment.Enrollment en)
    {
        return new EnrollmentDto
        {
            EnrollmentId = en.Id,
            StudentId = en.StudentId,
            CourseId = en.CourseId,
            EnrolledAt = en.CreatedAt,
        };
    }

    public static WithdrawalDto MapToWithdrawalDto(this EnrollmentWithdrawal enrollmentWithdrawal)
    {
        return new WithdrawalDto
        {
            WithdrawalId = enrollmentWithdrawal.Id,
            EnrollmentId = enrollmentWithdrawal.EnrollmentId,
            RefundRequestId = enrollmentWithdrawal.RefundRequestId,
            WithdrawalReason = enrollmentWithdrawal.WithdrawalReason,
            CreatedAt = enrollmentWithdrawal.CreatedAt,
            CreatorId = enrollmentWithdrawal.CreatedBy,
        };
    }

    public static EnrollmentDismissalDto MapToEnDismissalDto(this EnrollmentDismissal dismissal)
    {
        return new EnrollmentDismissalDto
        {
            Id = dismissal.Id,
            EnrollmentId = dismissal.EnrollmentId,
            DismissalTypeId = dismissal.DismissalTypeId,
            DismissalReason = dismissal.DismissalReason,
            CreatedAt = dismissal.CreatedAt,
            CreatorId = dismissal.CreatedBy,
        };
    }
    public static DismissalTypeDto MapToDismissalTypeDto(this DismissalType dismissalType)
    {
        return new DismissalTypeDto
        {
            TypeId = dismissalType.Id,
            Name = dismissalType.Name,
            Description = dismissalType.Description,
            CreatedAt = dismissalType.CreatedAt,
            CreatorId = dismissalType.CreatedBy,
        };
    }

    public static void MapDismissEnrollment(this EnrollmentDismissal dismissal, DismissEnrollmentDto dto)
    {
        dismissal.EnrollmentId = dto.EnrollmentId;
        dismissal.DismissalTypeId = dto.DismissalTypeId;
        dismissal.DismissalReason = dto.DismissalReason;
    }
}