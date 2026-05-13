namespace Faradars.Application.DTOs.Users.Address.AddressService;

public class ProvinceDto
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    
}