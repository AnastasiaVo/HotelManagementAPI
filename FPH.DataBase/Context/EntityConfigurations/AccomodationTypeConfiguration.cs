using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class AccomodationTypeConfiguration : IEntityTypeConfiguration<AccomodationTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AccomodationTypeEntity> builder)
        {
            builder.ToTable("AccomodationType");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NumberOfBeds).IsRequired();
        }
    }
}
