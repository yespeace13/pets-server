using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetsServer.Migrations
{
    /// <inheritdoc />
    public partial class initpetsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plan_content_act_act_id",
                table: "plan_content");

            migrationBuilder.AlterColumn<int>(
                name: "act_id",
                table: "plan_content",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_plan_content_act_act_id",
                table: "plan_content",
                column: "act_id",
                principalTable: "act",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plan_content_act_act_id",
                table: "plan_content");

            migrationBuilder.AlterColumn<int>(
                name: "act_id",
                table: "plan_content",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_plan_content_act_act_id",
                table: "plan_content",
                column: "act_id",
                principalTable: "act",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
