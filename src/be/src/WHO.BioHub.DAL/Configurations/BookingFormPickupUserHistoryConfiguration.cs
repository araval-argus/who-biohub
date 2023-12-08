using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class BookingFormPickupUserHistoryConfiguration : IEntityTypeConfiguration<BookingFormPickupUserHistory>
{
    public void Configure(EntityTypeBuilder<BookingFormPickupUserHistory> entity)
    {
        entity.Property(x => x.Other)
           .HasMaxLength(255)
           .IsRequired(false);

        entity.HasKey(bp => new { bp.BookingFormHistoryId, bp.UserId });

        entity.HasOne<BookingFormHistory>(bp => bp.BookingFormHistory)
            .WithMany(b => b.BookingFormPickupUsersHistory)
            .HasForeignKey(bp => bp.BookingFormHistoryId);


        entity.HasOne<User>(bp => bp.User)
            .WithMany(p => p.BookingFormPickupUsersHistory)
            .HasForeignKey(bp => bp.UserId);
    }

    public new static BookingFormPickupUserHistoryConfiguration Default => new BookingFormPickupUserHistoryConfiguration();
}
