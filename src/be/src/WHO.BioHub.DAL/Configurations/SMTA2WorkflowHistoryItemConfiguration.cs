using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class SMTA2WorkflowHistoryItemConfiguration : IEntityTypeConfiguration<SMTA2WorkflowHistoryItem>
{
    public static SMTA2WorkflowHistoryItemConfiguration Default => new();

    public void Configure(EntityTypeBuilder<SMTA2WorkflowHistoryItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_Status");


        builder
            .HasOne(e => e.Laboratory)
            .WithMany(b => b.SMTA2WorkflowHistoryItems)
            .HasForeignKey(e => e.LaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.LastOperationUser)
            .WithMany(b => b.SMTA2WorkflowHistoryItems)
            .HasForeignKey(e => e.LastOperationUserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.SMTA2WorkflowItem)
            .WithMany(b => b.SMTA2WorkflowHistoryItems)
            .HasForeignKey(e => e.SMTA2WorkflowItemId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
