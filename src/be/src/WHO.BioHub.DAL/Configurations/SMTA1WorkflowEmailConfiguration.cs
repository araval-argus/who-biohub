using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class SMTA1WorkflowEmailConfiguration : IEntityTypeConfiguration<SMTA1WorkflowEmail>
{
    public static SMTA1WorkflowEmailConfiguration Default => new();

    public void Configure(EntityTypeBuilder<SMTA1WorkflowEmail> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.FromStatus)
            .HasDatabaseName("IX_FromStatus");

        builder.HasIndex(e => e.ToStatus)
            .HasDatabaseName("IX_ToStatus");

        builder.HasIndex(e => e.ApprovedSubmission)
            .HasDatabaseName("IX_ApprovedSubmission");

        builder
            .HasOne(e => e.Role)
            .WithMany(b => b.SMTA1WorkflowEmails)
            .HasForeignKey(e => e.RoleId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
