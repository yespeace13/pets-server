using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Locality.Model;
using PetsServer.Domain.Organization.Model;
using System.Reflection.Metadata;

namespace PetsServer.Infrastructure.Context
{
    // dotnet ef migrations add InitialCreate добавить миграцию
    // dotnet ef database update применить миграцию для бд
    // Добавить в миграцию migrationBuilder.Sql(File.ReadAllText("InitData.sql"));
    public class PetsContext : DbContext
    {
        public DbSet<TypeOrganizationModel> TypeOrganizations { get; set; }

        public DbSet<LegalTypeModel> LegalTypes { get; set; }

        public DbSet<LocalityModel> Localities { get; set; }

        public DbSet<OrganizationModel> Organizations { get; set; }

        public DbSet<AnimalModel> Animals { get; set; }

        public DbSet<EntityPossibilities> EntityRestrictions { get; set; }

        public DbSet<RoleModel> Roles { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<ContractModel> Contracts { get; set; }

        public DbSet<ContractContentModel> ContractContents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=pets;User Id=postgres;Password=1234;Include Error Detail=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ContractModel>()
            //    .HasMany(e => e.ContractContents)
            //    .WithOne(e => e.Contract)
            //    .HasForeignKey(e => e.ContractId)
            //    .IsRequired(true);


            modelBuilder.UseSerialColumns();
            // Ограничения
            // Уникальность логина у пользователей
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Login)
                .IsUnique();

            // Уникальность населенного пункта
            modelBuilder.Entity<LocalityModel>()
                .HasIndex(l => l.Name)
                .IsUnique();

            // Уникальность типа организации
            modelBuilder.Entity<TypeOrganizationModel>()
                .HasIndex(l => l.Name)
                .IsUnique();

            // Уникальность юридического статуса организации
            modelBuilder.Entity<LegalTypeModel>()
                .HasIndex(l => l.Name)
                .IsUnique();

            // Уникальность роли
            modelBuilder.Entity<RoleModel>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Уникальность контракта по номеру, дате заключения, исполнителю и заказчику
            modelBuilder.Entity<ContractModel>()
                .HasIndex(c => new { c.Number, c.ClientId, c.ExecutorId, c.DateValid })
                .IsUnique();

            // Уникальность для контракта, что на один населенный пункт не 2 цены
            modelBuilder.Entity<ContractContentModel>()
                .HasIndex(c => new { c.LocalityId, c.ContractId })
                .IsUnique();

            // Конвертор
            // Для роли
            modelBuilder.Entity<EntityPossibilities>()
                .Property(e => e.Possibility)
                .HasConversion(new EnumToStringConverter<Possibilities>());

            modelBuilder.Entity<EntityPossibilities>()
                .Property(e => e.Entity)
                .HasConversion(new EnumToStringConverter<Entities>());

            modelBuilder.Entity<EntityPossibilities>()
                .Property(e => e.Restriction)
                .HasConversion(new EnumToStringConverter<Restrictions>());


        }
    }
}
