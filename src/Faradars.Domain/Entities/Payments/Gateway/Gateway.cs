using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Gateway;

public class Gateway : BaseEntity
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int Priority { get; set; } // which gateway comes first
    
}