using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Users.Domain.Roles;
using Stockify.Modules.Users.Infrastructure.Database.Constants;

namespace Stockify.Modules.Users.Infrastructure.Database.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Code);
        
        builder.Property(p => p.Code).HasMaxLength(100);
        
        builder.HasData(
            Permission.AccessUsers,
            Permission.AccessEverything);

        builder.HasMany<Role>()
            .WithMany(r => r.Permissions)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");
                
                joinBuilder.Property("PermissionsCode").HasColumnName("permission_code");

                joinBuilder.HasData(
                    CreateRolePermissions(Role.Administrator, Permission.AccessEverything));
            });
    }
    
    private static object CreateRolePermissions(Role role, Permission permission)
    {
        return new
        {
            RoleId = role.Id,
            PermissionsCode = permission.Code
        };
    }
}
