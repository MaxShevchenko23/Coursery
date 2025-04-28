using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursery.Domain.EntityConfigurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> entity)
    {
        entity
            .HasOne(e => e.Module)
            .WithMany()
            .HasForeignKey(e=>e.ModuleId);
        
        
    }
}