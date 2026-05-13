using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Transaction;

/// <summary>
/// Transaction type:
/// 1 = Payment (user pays)
/// 2 = Refund (money back)
/// 3 = Settlement (platform pays instructor)
/// 4 = EnrollmentWithdrawal (user withdraws from wallet)
/// </summary>
public class TransactionType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}