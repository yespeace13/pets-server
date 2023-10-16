using Microsoft.EntityFrameworkCore;
using PetsServer.Animal.Model;
using PetsServer.Locality.Model;
using PetsServer.Organization.Model;

namespace PetsServer.Context
{
    public class PetsContext : DbContext
    {
        public DbSet<TypeOrganizationModel> TypeOrganizations { get; set; }

        public DbSet<LegalTypeModel> LegalTypes { get; set; }

        public DbSet<LocalityModel> Localities { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }

        public DbSet<AnimalModel> Animals { get; set; }


        // Добавить в миграцию migrationBuilder.Sql(File.ReadAllText("InitData.sql"));
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=pets;User Id=postgres;Password=1234;Include Error Detail=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            //modelBuilder.UseIdentityAlwaysColumns();
            //modelBuilder.UseIdentityByDefaultColumns();
        }
    }
}
