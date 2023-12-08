using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class BSLLevelConfiguration : IEntityTypeConfiguration<BSLLevel>
{
    public static BSLLevelConfiguration Default => new();

    public void Configure(EntityTypeBuilder<BSLLevel> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Id)
            .HasDatabaseName("IX_Id")
            .IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Code)
            .HasDatabaseName("IX_Code");

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.Property(e => e.Code)
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);
    }
}
