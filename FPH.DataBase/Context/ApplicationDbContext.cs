using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FPH.DataBase.Context.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data;

namespace FPH.DataBase.Context
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AccomodationTypeEntity> AccomodationTypes { get; set; }

        public virtual DbSet<HotelRoomEntity> HotelRooms { get; set; }

        //public virtual DbSet<RoomFeePerNightEntity> RoomFeePerNights { get; set; }

        public virtual DbSet<BookingEntity> HotelBookings { get; set; }

        public virtual DbSet<PaymentEntity> Payments { get; set; }
        public virtual DbSet<PaymentTypeEntity> PaymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new AccomodationTypeConfiguration())
                .ApplyConfiguration(new RoomConfiguration())
                .ApplyConfiguration(new BookingConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                //.ApplyConfiguration(new RoomFeePerNightConfiguration())
                .ApplyConfiguration(new PaymentConfiguration())
                .ApplyConfiguration(new PaymentTypeConfiguration())
                .ApplyConfiguration(new RoleConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
    }
}
