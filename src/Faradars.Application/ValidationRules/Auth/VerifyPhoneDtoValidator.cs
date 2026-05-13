using Faradars.Application.DTOs.Auth;
using FluentValidation;

namespace Faradars.Application.ValidationRules.Auth
{
    public class VerifyPhoneDtoValidator : AbstractValidator<VerifyPhoneDto>
    {
        public VerifyPhoneDtoValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("شماره تلفن نمی‌تواند خالی باشد.")
                .Matches(@"^09\d{9}$")
                .WithMessage("شماره تلفن معتبر نیست. فرمت صحیح: 11 رقم و شروع با 09");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("کد تأیید نمی‌تواند خالی باشد.")
                .Matches(@"^\d{4,6}$")
                .WithMessage("کد تأیید باید فقط عدد و بین ۴ تا ۶ رقم باشد.");
        }
    }
}