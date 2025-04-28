using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursery.Domain.EntityConfigurations;

public class EnrollConfiguration : IEntityTypeConfiguration<Enroll>
{
    public void Configure(EntityTypeBuilder<Enroll> entity)
    {
        entity
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId);
        
        entity
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(e => e.CourseId);
     }
}