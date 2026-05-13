using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Interfaces;

namespace Faradars.Domain.Entities.Users.Address;

public class UserAddress : BaseEntity, IAdminInterference
{
    public int UserId { get; set; }
    public int CityId { get; set; }
    public string PostalCode { get; set; } = null!;
    public string FullAddress { get; set; } = null!;

    // Admin Interference Properties
    public bool? IsAccepted { get; set; } = true;
    public string? RejectionCause { get; set; }
    public DateTime? RejectionDate { get; set; }
    public int? RelatedAdminId { get; set; }

    public User User { get; set; } = null!;
    [ForeignKey("CityId")] public City City { get; set; } = null!;
}