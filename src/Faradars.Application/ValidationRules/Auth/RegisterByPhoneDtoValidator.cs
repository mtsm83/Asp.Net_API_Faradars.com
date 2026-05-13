using Faradars.Application.DTOs.Auth;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Auth;

public class RegisterByPhoneDtoValidator
    : AbstractValidator<RegisterByPhoneDto>
{
    public RegisterByPhoneDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("نام کاربری الزامی است")
            .MinimumLength(3).WithMessage("نام کاربری حداقل باید ۳ کاراکتر باشد")
            .MaximumLength(50);
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد");
        
        RuleFor(x => x.Phone)
            .Matches(@"^09\d{9}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("شماره موبایل معتبر نیست");
    }
}
