﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetsServer.Infrastructure.Context;

#nullable disable

namespace PetsServer.Migrations
{
    [DbContext(typeof(PetsContext))]
    partial class PetsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("PetsServer.Animal.Model.AnimalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Breed")
                        .HasColumnType("text")
                        .HasColumnName("breed");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("category");

                    b.Property<string>("ChipNumber")
                        .HasColumnType("text")
                        .HasColumnName("chip_number");

                    b.Property<string>("Color")
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<string>("Ears")
                        .HasColumnType("text")
                        .HasColumnName("ears");

                    b.Property<string>("IdentificationLabel")
                        .HasColumnType("text")
                        .HasColumnName("identification_label");

                    b.Property<bool?>("Sex")
                        .HasColumnType("boolean")
                        .HasColumnName("sex");

                    b.Property<double?>("Size")
                        .HasColumnType("double precision")
                        .HasColumnName("size");

                    b.Property<string>("SpecialSigns")
                        .HasColumnType("text")
                        .HasColumnName("special_signs");

                    b.Property<string>("Tail")
                        .HasColumnType("text")
                        .HasColumnName("tail");

                    b.Property<string>("Wool")
                        .HasColumnType("text")
                        .HasColumnName("wool");

                    b.HasKey("Id");

                    b.ToTable("animal");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.EntityPossibilities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("entity");

                    b.Property<string>("Possibility")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("possibility");

                    b.Property<string>("Restriction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("restriction");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("entity_possibilities");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("role");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organization_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("LocalityId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RoleId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("PetsServer.Locality.Model.LocalityModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("locality");
                });

            modelBuilder.Entity("PetsServer.Organization.Model.LegalTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("legal_type");
                });

            modelBuilder.Entity("PetsServer.Organization.Model.OrganizationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("INN")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inn");

                    b.Property<string>("KPP")
                        .HasColumnType("text")
                        .HasColumnName("kpp");

                    b.Property<int>("LegalTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("legal_type_id");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<string>("NameOrganization")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_organization");

                    b.Property<int>("TypeOrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("type_organization_id");

                    b.HasKey("Id");

                    b.HasIndex("LegalTypeId");

                    b.HasIndex("LocalityId");

                    b.HasIndex("TypeOrganizationId");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("PetsServer.Organization.Model.TypeOrganizationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("type_organization");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.EntityPossibilities", b =>
                {
                    b.HasOne("PetsServer.Authorization.Model.RoleModel", "Role")
                        .WithMany("Possibilities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.UserModel", b =>
                {
                    b.HasOne("PetsServer.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Organization.Model.OrganizationModel", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Authorization.Model.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Locality");

                    b.Navigation("Organization");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetsServer.Organization.Model.OrganizationModel", b =>
                {
                    b.HasOne("PetsServer.Organization.Model.LegalTypeModel", "LegalType")
                        .WithMany()
                        .HasForeignKey("LegalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Organization.Model.TypeOrganizationModel", "TypeOrganization")
                        .WithMany()
                        .HasForeignKey("TypeOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LegalType");

                    b.Navigation("Locality");

                    b.Navigation("TypeOrganization");
                });

            modelBuilder.Entity("PetsServer.Authorization.Model.RoleModel", b =>
                {
                    b.Navigation("Possibilities");
                });
#pragma warning restore 612, 618
        }
    }
}
