using Faradars.Domain.Entities.Courses.Discussion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faradars.Infrastructure.Persistence.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.ToTable("Answers");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Body)
            .IsRequired()
            .HasMaxLength(2000);
        
        // -----------------------------
        // Admin (ادمینی که بررسی کرده)
        // -----------------------------
        builder.HasOne(a => a.Admin)
            .WithMany()
            .HasForeignKey(a => a.RelatedAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // -----------------------------
        // CourseQuestion
        // -----------------------------
        builder.HasOne(a => a.CourseQuestion)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
