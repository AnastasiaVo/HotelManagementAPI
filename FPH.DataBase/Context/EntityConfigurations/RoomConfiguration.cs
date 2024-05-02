using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FPH.Data.Entities;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<HotelRoomEntity>
    {
        public void Configure(EntityTypeBuilder<HotelRoomEntity> builder)
        {
            builder.ToTable("HotelRoom");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.RoomNumber).IsRequired();
            builder.Property(r => r.Capacity).IsRequired();
            builder.Property(r => r.IsReserved)
                .HasDefaultValue(false)
                .IsRequired();

            // Relationships
            builder.HasOne(hr => hr.AccomodationTypeEntity)
                .WithMany(t => t.HotelRoomEntities)
                .HasForeignKey(hr => hr.AccomodationTypeEntityId);

        }
    }
}
