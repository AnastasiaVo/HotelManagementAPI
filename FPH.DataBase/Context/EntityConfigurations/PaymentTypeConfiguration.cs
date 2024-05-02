using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentTypeEntity> builder)
        {
            builder.ToTable("PaymentType");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Type).IsRequired().HasMaxLength(100);
        }
    }
}
