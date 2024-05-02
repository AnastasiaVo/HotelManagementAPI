//using FPH.DataBase.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace FavorParkHotel.Data
//{
//    public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            var connectionString = configuration.GetConnectionString("DefaultConnection");
//            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("FPH.DataBase"));

//            return new ApplicationDbContext(builder.Options);
//        }

//    }
//}
