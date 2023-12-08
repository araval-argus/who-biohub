using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class UserRequestConfiguration : IEntityTypeConfiguration<UserRequest>
{
    public static UserRequestConfiguration Default => new();

    public void Configure(EntityTypeBuilder<UserRequest> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.FirstName)
            .HasMaxLength(255)
            .IsRequired(true); ;
        builder.Property(e => e.LastName)
            .HasMaxLength(255)
            .IsRequired(true); ;
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired(true); ;
        builder.Property(e => e.Purpose)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder
          .HasOne(e => e.Role)
          .WithMany(b => b.UserRequests)
          .HasForeignKey(e => e.RoleId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(true);

        builder
          .HasOne(e => e.Country)
          .WithMany(b => b.UserRequests)
          .HasForeignKey(e => e.CountryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(true);

        builder.Property(e => e.Message)
            .HasMaxLength(5000)
            .IsRequired(false);

        builder.Property(e => e.InstituteName)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.RegistrationDate)
            .IsRequired(false);

        builder.Property(e => e.ConfirmationDate)
            .IsRequired(false);

        builder
          .HasOne(e => e.Laboratory)
          .WithMany(b => b.UserRequests)
          .HasForeignKey(e => e.LaboratoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);
    }
}
