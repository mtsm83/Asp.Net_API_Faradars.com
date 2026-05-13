using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Users.Address;

public class Province: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public ICollection<City> Cities { get; set; } = new List<City>();
    
}