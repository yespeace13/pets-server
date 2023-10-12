using IS_5.Organization.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SupportLibrary.Model.Locality;
using SupportLibrary.Model.Organization;

namespace PetsServer.Context
{
    public class PetsContext : DbContext
    {
        public DbSet<TypeOrganizationModel> TypeOrganizations { get; set; }

        public DbSet<LegalTypeModel> LegalTypes { get; set; }

        public DbSet<LocalityModel> Localities { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=pets;User Id=postgres;Password=1234;");

        protected void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(File.ReadAllText("")) ;

        }

    }
}
