namespace Faradars.Domain.Enums;

public enum RequestStatus
{
    Pending = 0,
    Rejected = 1,
    Approved = 2,
    AcceptedAndRefunded = 3,
    AcceptedNotRefunded = 4,
    Completed = 5,
    Cancelled = 6
}