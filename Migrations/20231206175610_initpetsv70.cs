using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetsServer.Migrations
{
    /// <inheritdoc />
    public partial class initpetsv70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firts_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "organization",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "organization",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "animal_file",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    parent_id = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_animal_file_animal_parent_id",
                        column: x => x.parent_id,
                        principalTable: "animal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_animal_file_parent_id",
                table: "animal_file",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animal_file");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "user");

            migrationBuilder.DropColumn(
                name: "department",
                table: "user");

            migrationBuilder.DropColumn(
                name: "email",
                table: "user");

            migrationBuilder.DropColumn(
                name: "firts_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "middle_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "position",
                table: "user");

            migrationBuilder.DropColumn(
                name: "email",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "organization");
        }
    }
}
