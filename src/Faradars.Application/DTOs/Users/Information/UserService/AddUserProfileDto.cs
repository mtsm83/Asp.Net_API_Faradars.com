using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Users.Information.UserService;

public class AddUserProfileDto
{
    public int UserId { get; set; }
    public required IFormFile File { get; set; }
}