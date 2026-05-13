using Faradars.Application.DTOs.Auth;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Auth
{
    public class VerifyEmailDtoValidator : AbstractValidator<VerifyEmailDto>
    {
        public VerifyEmailDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("ایمیل نمی‌تواند خالی باشد.")
                .EmailAddress()
                .WithMessage("فرمت ایمیل معتبر نیست.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("کد تأیید نمی‌تواند خالی باشد.")
                .Matches(@"^\d{4,6}$")
                .WithMessage("کد تأیید باید فقط عدد و بین ۴ تا ۶ رقم باشد.");
        }
    }
}