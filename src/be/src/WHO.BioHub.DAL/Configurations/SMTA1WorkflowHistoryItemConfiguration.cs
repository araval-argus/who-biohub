using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;


internal class SMTA1WorkflowHistoryItemConfiguration : IEntityTypeConfiguration<SMTA1WorkflowHistoryItem>
{
    public static SMTA1WorkflowHistoryItemConfiguration Default => new();

    public void Configure(EntityTypeBuilder<SMTA1WorkflowHistoryItem> builder)
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
            .WithMany(b => b.SMTA1WorkflowHistoryItems)
            .HasForeignKey(e => e.LaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.LastOperationUser)
            .WithMany(b => b.SMTA1WorkflowHistoryItems)
            .HasForeignKey(e => e.LastOperationUserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.SMTA1WorkflowItem)
            .WithMany(b => b.SMTA1WorkflowHistoryItems)
            .HasForeignKey(e => e.SMTA1WorkflowItemId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}

