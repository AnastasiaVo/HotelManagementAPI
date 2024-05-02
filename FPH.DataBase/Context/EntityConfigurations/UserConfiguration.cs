using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");


            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();

            builder.HasMany(u => u.HotelRoom)
                    .WithOne(hr => hr.Users);
        }
    }
}
