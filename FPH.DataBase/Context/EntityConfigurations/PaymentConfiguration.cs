using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPH.DataBase.Context.EntityConfigurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsPaid).IsRequired();
            builder.Property(p => p.PaymentAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(decimal.Zero)
                .IsRequired();

            builder.HasOne(p => p.PaymentType)
                .WithOne(p => p.Payment)
                .HasForeignKey<PaymentEntity>(p => p.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
