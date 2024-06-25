using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Users.Domain.Roles;
using Stockify.Modules.Users.Infrastructure.Database.Constants;

namespace Stockify.Modules.Users.Infrastructure.Database.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).HasMaxLength(100);

        builder.HasData(
            Role.User,
            Role.Manager,
            Role.Administrator);
    }
}
