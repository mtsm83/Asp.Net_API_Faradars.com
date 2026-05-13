using Faradars.Domain.Interfaces;

namespace Faradars.Domain.BaseClasses;

public abstract class BaseEntity<TKey> : IEntity, IAuditableEntity<TKey>
    , ISoftDeleteableEntity

{
    public TKey Id { get; set; }
    public TKey? CreatedBy { get; set; }
    public TKey? UpdatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}