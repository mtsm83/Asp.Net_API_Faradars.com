using FluentValidation;
using Faradars.Application.DTOs.Courses.Content.CourseService;

namespace Faradars.Application.ValidationRules.Courses.CourseService;

public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseDtoValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0)
            .WithMessage("شناسه‌ی دوره معتبر نیست.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان دوره الزامی است.")
            .MaximumLength(200).WithMessage("عنوان دوره نمی‌تواند بیش از ۲۰۰ کاراکتر باشد.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات دوره الزامی است.")
            .MaximumLength(5000).WithMessage("توضیحات دوره نمی‌تواند بیش از ۵۰۰۰ کاراکتر باشد.");

        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("سطح دوره الزامی است.")
            .MaximumLength(100).WithMessage("سطح دوره نمی‌تواند بیش از ۱۰۰ کاراکتر باشد.");

        RuleFor(x => x.Language)
            .MaximumLength(50).WithMessage("زبان دوره نمی‌تواند بیش از ۵۰ کاراکتر باشد.");

        RuleFor(x => x.TargetAudience)
            .NotEmpty().WithMessage("مخاطبان هدف الزامی هستند.")
            .MaximumLength(300).WithMessage("مخاطبان هدف نمی‌تواند بیش از ۳۰۰ کاراکتر باشند.");

        RuleFor(x => x.CourseType)
            .NotEmpty().WithMessage("نوع دوره الزامی است.")
            .MaximumLength(100).WithMessage("نوع دوره نمی‌تواند بیش از ۱۰۰ کاراکتر باشد.");

        RuleFor(x => x.TeacherId)
            .GreaterThan(0)
            .WithMessage("شناسه‌ی مدرس معتبر نیست.");
    }
}