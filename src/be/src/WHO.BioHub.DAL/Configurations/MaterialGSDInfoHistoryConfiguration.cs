using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class MaterialGSDInfoHistoryConfiguration : IEntityTypeConfiguration<MaterialGSDInfoHistory>
{
    public static MaterialGSDInfoHistoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<MaterialGSDInfoHistory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();       



        builder.Property(e => e.CellLine)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.GSDFasta)            
            .IsRequired(false);



        builder.Property(e => e.GSDType)          
            .IsRequired(false);

        builder.Property(e => e.PassageNumber)
            .IsRequired(false);

        builder
          .HasOne(e => e.MaterialHistory)
          .WithMany(b => b.MaterialGSDInfo)
          .HasForeignKey(e => e.MaterialHistoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);              

    }
}
