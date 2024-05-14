using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.CheckInDate).IsRequired();
            builder.Property(b => b.CheckOutDate).IsRequired();

            builder.HasOne(b => b.PaymentEntity)
                .WithOne(p => p.Booking)
                .HasForeignKey<PaymentEntity>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(b => b.HotelRooms)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .IsRequired();

            builder.Property(p => p.RoomFeePerNight)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(decimal.Zero)
                .IsRequired();
        }
    }
}
