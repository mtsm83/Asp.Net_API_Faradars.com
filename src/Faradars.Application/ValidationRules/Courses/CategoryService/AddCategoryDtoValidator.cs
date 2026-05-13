using Faradars.Application.DTOs.Courses.Category.CategoryService;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Courses.CategoryService;

public class AddCategoryDtoValidator 
    : AbstractValidator<AddCategoryDto>
{
    public AddCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("عنوان دسته‌بندی الزامی است")
            .MaximumLength(100).WithMessage("عنوان بیش از حد مجاز طولانی است");
    }
}
