namespace Faradars.Domain.Interfaces;

public interface ISoftDeleteableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}