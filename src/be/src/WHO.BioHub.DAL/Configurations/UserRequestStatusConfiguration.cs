using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class UserRequestStatusConfiguration : IEntityTypeConfiguration<UserRequestStatus>
{
    public static UserRequestStatusConfiguration Default => new();

    public void Configure(EntityTypeBuilder<UserRequestStatus> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Message)
            .HasMaxLength(5000)
            .IsRequired(true);
        builder.Property(e => e.Status)
            .IsRequired(true);
        builder.Property(e => e.IsResponseMessage)
            .IsRequired(true);
    }
}
