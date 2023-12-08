using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class SMTA1WorkflowItemConfiguration : IEntityTypeConfiguration<SMTA1WorkflowItem>
{
    public static SMTA1WorkflowItemConfiguration Default => new();

    public void Configure(EntityTypeBuilder<SMTA1WorkflowItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_Status");


        builder.Property(e => e.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder
            .HasOne(e => e.Laboratory)
            .WithMany(b => b.SMTA1WorkflowItems)
            .HasForeignKey(e => e.LaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.LastOperationUser)
            .WithMany(b => b.SMTA1WorkflowItems)
            .HasForeignKey(e => e.LastOperationUserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
