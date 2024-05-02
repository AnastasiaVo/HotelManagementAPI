using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPH.DataBase.Context.EntityConfigurations;
internal class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        var roles = new List<RoleEntity>()
        {
            new()
            {
                Id = "Admin",
                Name = "Admin",
                NormalizedName = "Admin"
            },
            new()
            {
                Id = "User",
                Name = "User",
                NormalizedName = "User"
            }
        };
        builder.HasData(roles);
    }
}
