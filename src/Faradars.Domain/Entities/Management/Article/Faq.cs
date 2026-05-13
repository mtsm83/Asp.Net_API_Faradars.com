using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Management.Article;

public class Faq : BaseEntity
{
    public string QuestionTitle { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string Order { get; set; } = null!;
}