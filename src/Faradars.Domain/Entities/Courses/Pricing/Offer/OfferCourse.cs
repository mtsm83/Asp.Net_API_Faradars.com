using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Pricing.Offer;

public class OfferCourse : BaseEntity
{
    public int CourseId { get; set; }
    public int OfferId { get; set; }

    public Course Course { get; set; } 
    public Offer Offer { get; set; }
}