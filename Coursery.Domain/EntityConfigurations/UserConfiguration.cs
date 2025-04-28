using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coursery.Domain.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>

{
    public void Configure(EntityTypeBuilder<User> entity)
    {

    }
}