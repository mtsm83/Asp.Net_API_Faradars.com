namespace Faradars.Application.DTOs.Users.Address.AddressService;

public class UserAddressDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CityId { get; set; }
    public int ProvinceId { get; set; }
    public string PostalCode { get; set; } = null!;
    public string FullAddress { get; set; } = null!;
    public int CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
}