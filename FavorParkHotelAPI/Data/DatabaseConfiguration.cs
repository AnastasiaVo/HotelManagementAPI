using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FPH.Common.Options;
using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using FPH.DataBase.Repositories;
using FavorParkHotelAPI.Application.BookingManagement;

namespace FavorParkHotelAPI.Data;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtection();

        var connectionStrings = new ConnectionStringOptions(configuration);

        services.AddDbContext(connectionStrings.DefaultConnection);
        services.ConfigureIdentity();

        services.AddTransient<UserManager<UserEntity>>();
        services.AddTransient<RoleManager<RoleEntity>>();

        services.ConfigureRepositories();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(RoleEntity), builder.Services);

        builder.AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHotelRoomRepository, HotelRoomRepository>();
        services.AddScoped<IAccomodationTypeRepository, AccomodationTypeRepository>();
        services.AddScoped<IBookingRepository, BookingsRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
        services.AddTransient<RoomFeeCalculationService>();
    }
}
