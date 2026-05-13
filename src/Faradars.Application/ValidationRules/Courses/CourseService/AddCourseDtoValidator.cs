using Faradars.Application.DTOs.Courses.Content.CourseService;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Courses.CourseService;

public class AddCourseDtoValidator
    : AbstractValidator<AddCourseDto>
{
    public AddCourseDtoValidator()
    {
        
        #region Teacher & Profile

        RuleFor(x => x.TeacherId)
            .GreaterThan(0).WithMessage("مدرس معتبر انتخاب نشده است");


        #endregion

        #region Language & Level

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("زبان دوره الزامی است");

        RuleFor(x => x.Level)
            .Must(BeValidLevel)
            .WithMessage("سطح دوره نامعتبر است");

        #endregion
    }

    // سطح دوره باید یکی از این مقدارها باشد
    private bool BeValidLevel(string level)
    {
        var validLevels = new[] { "Beginner", "Intermediate", "Advanced", "AllLevels" };
        return validLevels.Contains(level);
    }
}