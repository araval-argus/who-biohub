using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public static RoleConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(255);

        builder.Property(e => e.PublicName)
            .HasMaxLength(255)
            .IsRequired(false);

    }
}
