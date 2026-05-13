using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Users.Address;

public class City: BaseEntity
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = null!;
    
    public Province Province { get; set; } = null!;
}