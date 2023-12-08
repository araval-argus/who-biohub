using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public static UserConfiguration Default => new();

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.FirstName)
            .HasMaxLength(255)
            .IsRequired(true);
        builder.Property(e => e.LastName)
            .HasMaxLength(255)
            .IsRequired(true);
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired(true);
        builder.Property(e => e.JobTitle)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(e => e.MobilePhone)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(e => e.BusinessPhone)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Notes)
           .HasMaxLength(255)
           .IsRequired(false);

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_Email");

        builder.HasIndex(e => e.ExternalId)
            .HasDatabaseName("IX_ExternalId");

        builder.HasIndex(e => e.DeletedOn)
            .HasDatabaseName("IX_DeletedOn");

        builder.Property(e => e.ExternalId)
           .IsRequired(false);

        builder
          .HasOne(e => e.Role)
          .WithMany(b => b.Users)
          .HasForeignKey(e => e.RoleId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(true);

        builder
          .HasOne(e => e.Laboratory)
          .WithMany(b => b.Users)
          .HasForeignKey(e => e.LaboratoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.BioHubFacility)
          .WithMany(b => b.Users)
          .HasForeignKey(e => e.BioHubFacilityId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.Courier)
          .WithMany(b => b.Users)
          .HasForeignKey(e => e.CourierId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);
    }
}
