using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Management.Request.Content;

public class RequestSubject: BaseEntity
{
    // Refund because unsatisfaction of : course,
    // plan or refund due to not satisfaction as an instructor 
    public string Title { get; set; } = null!; 
    public string? Description { get; set; }
    
}