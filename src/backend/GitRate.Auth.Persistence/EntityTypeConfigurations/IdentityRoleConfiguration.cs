using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitRate.Auth.Persistence.EntityTypeConfigurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name).HasColumnName("name");

            builder.Property(x => x.NormalizedName).HasColumnName("normalized_name");

            builder.Property(x => x.ConcurrencyStamp).HasColumnName("concurrency_stamp");

            builder.ToTable("identity_roles");
        }
    }
}