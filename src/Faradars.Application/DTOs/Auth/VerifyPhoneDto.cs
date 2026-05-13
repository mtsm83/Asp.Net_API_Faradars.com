namespace Faradars.Application.DTOs.Auth;

public class VerifyPhoneDto
{
    public required string Phone { get; set; }
    public required string Code { get; set; }
}