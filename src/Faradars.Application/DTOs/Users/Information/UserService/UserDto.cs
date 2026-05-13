using Faradars.Application.DTOs.Auth;

namespace Faradars.Application.DTOs.Users.Information.UserService;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsPhoneVerified { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? NCode { get; set; }
    public string? ProfileImageUrl { get; set; }
    public int? ProfileImageId { get; set; }
    public AccessTokenDto? AccessTokenDto { get; set; }
    public DateTime? RegisteredAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
    
}