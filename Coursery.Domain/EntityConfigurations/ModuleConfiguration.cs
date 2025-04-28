using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module = Coursery.Domain.Entities.Module;

namespace Coursery.Domain.EntityConfigurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> entity)
    {
        entity
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(e => e.CourseId);
    }
}