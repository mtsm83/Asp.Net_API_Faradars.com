using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Payments.Cart;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public DateTime? ConvertedToOrderAt { get; set; }
    
    public User User { get; set; } = null!;
    public ICollection<CartItem>? Items { get; set; }
    public ICollection<Order.Order>? Orders { get; set; }
}