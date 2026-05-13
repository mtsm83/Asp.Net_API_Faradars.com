using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Content;

public class CoursePrerequisite : BaseEntity
{
    public int CourseId { get; set; }
    public int PrerequisiteCourseId { get; set; }

    [ForeignKey("CourseId")] public Course Course { get; set; } = null!;
    [ForeignKey("PrerequisiteCourseId")]
    public Course
        PrerequisiteCourse { get; set; } = null!;
}