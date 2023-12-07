﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetsServer.Infrastructure.Context;

#nullable disable

namespace PetsServer.Migrations
{
    [DbContext(typeof(PetsContext))]
    [Migration("20231207151202_initpetsv6")]
    partial class initpetsv6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.EntityPossibilities", b =>
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

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.RoleModel", b =>
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

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Department")
                        .HasColumnType("text")
                        .HasColumnName("department");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firts_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organization_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Phone");

                    b.Property<string>("Position")
                        .HasColumnType("text")
                        .HasColumnName("position");

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

            modelBuilder.Entity("PetsServer.Domain.Act.Model.ActModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractId")
                        .HasColumnType("integer")
                        .HasColumnName("contract_id");

                    b.Property<DateOnly>("DateOfCapture")
                        .HasColumnType("date")
                        .HasColumnName("date_of_capture");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("integer")
                        .HasColumnName("executor_id");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("LocalityId");

                    b.ToTable("act");
                });

            modelBuilder.Entity("PetsServer.Domain.Act.Model.ActPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("act_file");
                });

            modelBuilder.Entity("PetsServer.Domain.Animal.Model.AnimalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ActId")
                        .HasColumnType("integer")
                        .HasColumnName("act_id");

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

                    b.HasIndex("ActId");

                    b.ToTable("animal");
                });

            modelBuilder.Entity("PetsServer.Domain.Animal.Model.AnimalPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("animal_file");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractContentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractId")
                        .HasColumnType("integer")
                        .HasColumnName("contract_id");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("LocalityId", "ContractId")
                        .IsUnique();

                    b.ToTable("contract_content");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DateOnly>("DateOfConclusion")
                        .HasColumnType("date")
                        .HasColumnName("date_of_conclusion");

                    b.Property<DateOnly>("DateValid")
                        .HasColumnType("date")
                        .HasColumnName("date_valid");

                    b.Property<int>("ExecutorId")
                        .HasColumnType("integer")
                        .HasColumnName("executor_id");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("Number", "ClientId", "ExecutorId", "DateValid")
                        .IsUnique();

                    b.ToTable("contract");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("contract_file");
                });

            modelBuilder.Entity("PetsServer.Domain.Locality.Model.LocalityModel", b =>
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

            modelBuilder.Entity("PetsServer.Domain.Organization.Model.LegalTypeModel", b =>
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

            modelBuilder.Entity("PetsServer.Domain.Organization.Model.OrganizationModel", b =>
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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

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

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("TypeOrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("type_organization_id");

                    b.HasKey("Id");

                    b.HasIndex("LegalTypeId");

                    b.HasIndex("LocalityId");

                    b.HasIndex("TypeOrganizationId");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("PetsServer.Domain.Organization.Model.TypeOrganizationModel", b =>
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

            modelBuilder.Entity("PetsServer.Domain.Plan.Model.PlanContentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("ActId")
                        .HasColumnType("integer")
                        .HasColumnName("act_id");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("adress");

                    b.Property<bool>("Check")
                        .HasColumnType("boolean")
                        .HasColumnName("check");

                    b.Property<int>("Day")
                        .HasColumnType("integer")
                        .HasColumnName("day");

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<int>("PlanId")
                        .HasColumnType("integer")
                        .HasColumnName("plan_id");

                    b.HasKey("Id");

                    b.HasIndex("ActId");

                    b.HasIndex("LocalityId");

                    b.HasIndex("PlanId");

                    b.ToTable("plan_content");
                });

            modelBuilder.Entity("PetsServer.Domain.Plan.Model.PlanModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("Month")
                        .HasColumnType("integer")
                        .HasColumnName("month");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("plan");
                });

            modelBuilder.Entity("PetsServer.Domain.Report.Model.ReportContentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("LocalityId")
                        .HasColumnType("integer")
                        .HasColumnName("locality_id");

                    b.Property<int>("NumberOfAnimals")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_animals");

                    b.Property<int>("ReportId")
                        .HasColumnType("integer")
                        .HasColumnName("report_id");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric")
                        .HasColumnName("total_cost");

                    b.HasKey("Id");

                    b.HasIndex("LocalityId");

                    b.HasIndex("ReportId");

                    b.ToTable("report_content");
                });

            modelBuilder.Entity("PetsServer.Domain.Report.Model.ReportModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateEnd")
                        .HasColumnType("date")
                        .HasColumnName("date_end");

                    b.Property<DateOnly>("DateStart")
                        .HasColumnType("date")
                        .HasColumnName("date_start");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.ToTable("report");
                });

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.EntityPossibilities", b =>
                {
                    b.HasOne("PetsServer.Auth.Authorization.Model.RoleModel", "Role")
                        .WithMany("Possibilities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.UserModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Organization.Model.OrganizationModel", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Auth.Authorization.Model.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Locality");

                    b.Navigation("Organization");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetsServer.Domain.Act.Model.ActModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Contract.Model.ContractModel", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Organization.Model.OrganizationModel", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Executor");

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("PetsServer.Domain.Act.Model.ActPhoto", b =>
                {
                    b.HasOne("PetsServer.Domain.Act.Model.ActModel", "Entity")
                        .WithMany("Photos")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("PetsServer.Domain.Animal.Model.AnimalModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Act.Model.ActModel", "Act")
                        .WithMany("Animal")
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");
                });

            modelBuilder.Entity("PetsServer.Domain.Animal.Model.AnimalPhoto", b =>
                {
                    b.HasOne("PetsServer.Domain.Animal.Model.AnimalModel", "Entity")
                        .WithMany("Photos")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractContentModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Contract.Model.ContractModel", "Contract")
                        .WithMany("ContractContent")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Organization.Model.OrganizationModel", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Organization.Model.OrganizationModel", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractPhoto", b =>
                {
                    b.HasOne("PetsServer.Domain.Contract.Model.ContractModel", "Entity")
                        .WithMany("FIles")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("PetsServer.Domain.Organization.Model.OrganizationModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Organization.Model.LegalTypeModel", "LegalType")
                        .WithMany()
                        .HasForeignKey("LegalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Organization.Model.TypeOrganizationModel", "TypeOrganization")
                        .WithMany()
                        .HasForeignKey("TypeOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LegalType");

                    b.Navigation("Locality");

                    b.Navigation("TypeOrganization");
                });

            modelBuilder.Entity("PetsServer.Domain.Plan.Model.PlanContentModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Act.Model.ActModel", "Act")
                        .WithMany()
                        .HasForeignKey("ActId");

                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Plan.Model.PlanModel", "Plan")
                        .WithMany("PlanContent")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");

                    b.Navigation("Locality");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("PetsServer.Domain.Report.Model.ReportContentModel", b =>
                {
                    b.HasOne("PetsServer.Domain.Locality.Model.LocalityModel", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetsServer.Domain.Report.Model.ReportModel", "Report")
                        .WithMany("ReportContent")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locality");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("PetsServer.Auth.Authorization.Model.RoleModel", b =>
                {
                    b.Navigation("Possibilities");
                });

            modelBuilder.Entity("PetsServer.Domain.Act.Model.ActModel", b =>
                {
                    b.Navigation("Animal");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PetsServer.Domain.Animal.Model.AnimalModel", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("PetsServer.Domain.Contract.Model.ContractModel", b =>
                {
                    b.Navigation("ContractContent");

                    b.Navigation("FIles");
                });

            modelBuilder.Entity("PetsServer.Domain.Plan.Model.PlanModel", b =>
                {
                    b.Navigation("PlanContent");
                });

            modelBuilder.Entity("PetsServer.Domain.Report.Model.ReportModel", b =>
                {
                    b.Navigation("ReportContent");
                });
#pragma warning restore 612, 618
        }
    }
}
