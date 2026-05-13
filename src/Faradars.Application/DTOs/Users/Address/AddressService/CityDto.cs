namespace Faradars.Application.DTOs.Users.Address.AddressService;

public class CityDto
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public int CreatorId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}