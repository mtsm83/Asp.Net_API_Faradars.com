namespace Faradars.Application.DTOs.Users.Address.AddressService;

public class UpdateUserAddressDto
{
    public int AddressId { get; set; }
    public int CityId { get; set; }
    public string PostalCode { get; set; } = null!;
    public string FullAddress { get; set; } = null!;
}