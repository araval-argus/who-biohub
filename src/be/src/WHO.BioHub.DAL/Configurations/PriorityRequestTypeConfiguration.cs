using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class PriorityRequestTypeConfiguration : IEntityTypeConfiguration<PriorityRequestType>
{
    public static PriorityRequestTypeConfiguration Default => new();

    public void Configure(EntityTypeBuilder<PriorityRequestType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.HexColor)
            .HasMaxLength(100);
    }
}
