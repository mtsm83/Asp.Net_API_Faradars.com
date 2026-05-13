using Faradars.Application.DTOs.Auth;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Auth;

public class LoginByPhoneDtoValidator
    : AbstractValidator<LoginByPhoneDto>
{
    public LoginByPhoneDtoValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد");
        
        RuleFor(x => x.Phone)
            .Matches(@"^09\d{9}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("شماره موبایل معتبر نیست");
    }
}