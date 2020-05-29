using GitRate.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitRate.Auth.Persistence.EntityTypeConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            
            builder.Property(x => x.CreateDate)
                .HasColumnName("create_date")
                .HasDefaultValueSql("now() at time zone 'utc'");

            builder.Property(x => x.IsUsed)
                .HasColumnName("is_used");

            builder.Property(x => x.Jti)
                .IsRequired()
                .HasColumnName("jwt_jti");

            builder.Property(x => x.ExpireDate)
                .HasColumnName("expire_date");

            builder.Property(x => x.UserId)
                .HasColumnName("user_id");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.ToTable("refresh_tokens");
        }
    }
}