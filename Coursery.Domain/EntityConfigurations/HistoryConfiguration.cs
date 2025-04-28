using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursery.Domain.EntityConfigurations;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> entity)
    {
        entity
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId);

        entity
            .HasOne<Lesson>()
            .WithMany()
            .HasForeignKey(e => e.LessonId);
    }
}