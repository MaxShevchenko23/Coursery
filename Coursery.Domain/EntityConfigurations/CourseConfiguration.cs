using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursery.Domain.EntityConfigurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity
            .HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey(e => e.AuthorId);
    }
    
    
}