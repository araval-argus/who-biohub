using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistToBioHubEmailConfiguration : IEntityTypeConfiguration<WorklistToBioHubEmail>
{
    public static WorklistToBioHubEmailConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistToBioHubEmail> builder)
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
            .WithMany(b => b.WorklistToBioHubEmails)
            .HasForeignKey(e => e.RoleId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
