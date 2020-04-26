using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityTypeConfigurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.Property(x => x.UserId).HasColumnName("user_id");

            builder.Property(x => x.RoleId).HasColumnName("role_id");

            builder.ToTable("identity_user_roles");
        }
    }
}