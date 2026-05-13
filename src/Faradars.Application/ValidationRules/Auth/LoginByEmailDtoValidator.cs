using Faradars.Application.DTOs.Auth;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Auth;

public class LoginByEmailDtoValidator
    : AbstractValidator<LoginByEmailDto>
{
    public LoginByEmailDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("فرمت ایمیل معتبر نیست");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد");
    }
}