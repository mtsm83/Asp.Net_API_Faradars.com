namespace Faradars.Domain.Entities.Payments.Transaction;

public class PurchaseTransaction
{
    public int OrderId { get; set; }
    public int TransactionId { get; set; }
    
    public Transaction Transaction { get; set; }
    public Order.Order Order { get; set; }
    
}