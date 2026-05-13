using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Users.Information.UserService;

public class UpdateUserInfoDto
{
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public GenderType? Gender { get; set; }
    public string? NCode { get; set; }
}