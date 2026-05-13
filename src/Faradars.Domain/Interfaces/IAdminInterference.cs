namespace Faradars.Domain.Interfaces;

public interface IAdminInterference
{
    // in case if rejected or re-accepted most recent id of admin is restored
    public int? RelatedAdminId { get; set; } 
    public bool? IsAccepted { get; set; }
    public string? RejectionCause { get; set; }
    public DateTime? RejectionDate { get; set; }
}