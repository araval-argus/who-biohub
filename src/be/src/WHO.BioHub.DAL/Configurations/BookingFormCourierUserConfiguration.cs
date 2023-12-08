using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class BookingFormCourierUserConfiguration : IEntityTypeConfiguration<BookingFormCourierUser>
{
    public void Configure(EntityTypeBuilder<BookingFormCourierUser> entity)
    {
        entity.Property(x => x.Other)
            .HasMaxLength(255)
            .IsRequired(false);

        entity.HasKey(bp => new { bp.BookingFormId, bp.UserId });

        entity.HasOne<BookingForm>(bp => bp.BookingForm)
            .WithMany(b => b.BookingFormCourierUsers)
            .HasForeignKey(bp => bp.BookingFormId);


        entity.HasOne<User>(bp => bp.User)
            .WithMany(p => p.BookingFormCourierUsers)
            .HasForeignKey(bp => bp.UserId);
    }

    public new static BookingFormCourierUserConfiguration Default => new BookingFormCourierUserConfiguration();
}
