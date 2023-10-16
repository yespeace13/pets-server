using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetsServer.Migrations
{
    /// <inheritdoc />
    public partial class initpets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "animal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    category_animal = table.Column<string>(type: "text", nullable: false),
                    sex_animal = table.Column<bool>(type: "boolean", nullable: true),
                    breed_animal = table.Column<string>(type: "text", nullable: true),
                    size_animal = table.Column<double>(type: "double precision", nullable: true),
                    wool_animal = table.Column<string>(type: "text", nullable: true),
                    color_animal = table.Column<string>(type: "text", nullable: true),
                    ears_animal = table.Column<string>(type: "text", nullable: true),
                    tail_animal = table.Column<string>(type: "text", nullable: true),
                    specialSigns_animal = table.Column<string>(type: "text", nullable: true),
                    identificationLabel_animal = table.Column<string>(type: "text", nullable: true),
                    chipNumber_animal = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "legal_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_legal_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_organization",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name_organization = table.Column<string>(type: "text", nullable: false),
                    inn = table.Column<string>(type: "text", nullable: false),
                    kpp = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false),
                    type_organization_id = table.Column<int>(type: "integer", nullable: false),
                    legal_type_id = table.Column<int>(type: "integer", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_legal_type_legal_type_id",
                        column: x => x.legal_type_id,
                        principalTable: "legal_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_type_organization_type_organization_id",
                        column: x => x.type_organization_id,
                        principalTable: "type_organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_legal_type_id",
                table: "organization",
                column: "legal_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_locality_id",
                table: "organization",
                column: "locality_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_type_organization_id",
                table: "organization",
                column: "type_organization_id");

            migrationBuilder.Sql(File.ReadAllText("InitData.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animal");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "legal_type");

            migrationBuilder.DropTable(
                name: "locality");

            migrationBuilder.DropTable(
                name: "type_organization");
        }
    }
}
