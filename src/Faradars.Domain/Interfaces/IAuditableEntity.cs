namespace Faradars.Domain.Interfaces;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}

public interface IAuditableEntity<T> : IAuditableEntity
{
    T? CreatedBy { get; set; }
    T? UpdatedBy { get; set; }
}