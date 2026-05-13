using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Users.Information;

public class VerificationCode : BaseEntity
{
    public required string Code { get; set; }
    public int? UsedBy { get; set; }
    public DateTime? UsageTime { get; set; }
    public string? VerifiedResourceBy { get; set; } // can be either email - phone of the user
}