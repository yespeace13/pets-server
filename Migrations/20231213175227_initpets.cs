using System;
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
                name: "plan_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    status_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    status_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
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
                name: "plan",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan", x => x.id);
                    table.ForeignKey(
                        name: "FK_plan_plan_status_status_id",
                        column: x => x.status_id,
                        principalTable: "plan_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    date_start = table.Column<DateOnly>(type: "date", nullable: false),
                    date_end = table.Column<DateOnly>(type: "date", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    date_status = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_report_status_status_id",
                        column: x => x.status_id,
                        principalTable: "report_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_possibilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    entity = table.Column<string>(type: "text", nullable: false),
                    possibility = table.Column<string>(type: "text", nullable: false),
                    restriction = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_possibilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_entity_possibilities_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    phone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "report_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    total_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false),
                    number_of_animals = table.Column<int>(type: "integer", nullable: false),
                    report_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_content_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_report_content_report_report_id",
                        column: x => x.report_id,
                        principalTable: "report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    number = table.Column<string>(type: "text", nullable: false),
                    date_of_conclusion = table.Column<DateOnly>(type: "date", nullable: false),
                    date_valid = table.Column<DateOnly>(type: "date", nullable: false),
                    executor_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract", x => x.id);
                    table.ForeignKey(
                        name: "FK_contract_organization_client_id",
                        column: x => x.client_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contract_organization_executor_id",
                        column: x => x.executor_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    department = table.Column<string>(type: "text", nullable: true),
                    position = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_organization_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "act",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    executor_id = table.Column<int>(type: "integer", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false),
                    date_of_capture = table.Column<DateOnly>(type: "date", nullable: false),
                    contract_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_act", x => x.id);
                    table.ForeignKey(
                        name: "FK_act_contract_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_act_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_act_organization_executor_id",
                        column: x => x.executor_id,
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_contract_content_contract_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contract_content_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contract_file",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    parent_id = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_contract_file_contract_parent_id",
                        column: x => x.parent_id,
                        principalTable: "contract",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    action_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    entity = table.Column<int>(type: "integer", nullable: false),
                    entity_description = table.Column<string>(type: "text", nullable: false),
                    id_object = table.Column<int>(type: "integer", nullable: true),
                    id_file = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_log_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "act_file",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    parent_id = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_act_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_act_file_act_parent_id",
                        column: x => x.parent_id,
                        principalTable: "act",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "animal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    category = table.Column<string>(type: "text", nullable: false),
                    sex = table.Column<bool>(type: "boolean", nullable: true),
                    breed = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<double>(type: "double precision", nullable: true),
                    wool = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true),
                    ears = table.Column<string>(type: "text", nullable: true),
                    tail = table.Column<string>(type: "text", nullable: true),
                    special_signs = table.Column<string>(type: "text", nullable: true),
                    identification_label = table.Column<string>(type: "text", nullable: true),
                    chip_number = table.Column<string>(type: "text", nullable: true),
                    act_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal", x => x.id);
                    table.ForeignKey(
                        name: "FK_animal_act_act_id",
                        column: x => x.act_id,
                        principalTable: "act",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plan_content",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    day = table.Column<int>(type: "integer", nullable: false),
                    locality_id = table.Column<int>(type: "integer", nullable: false),
                    act_id = table.Column<int>(type: "integer", nullable: true),
                    adress = table.Column<string>(type: "text", nullable: false),
                    check = table.Column<bool>(type: "boolean", nullable: false),
                    plan_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_plan_content_act_act_id",
                        column: x => x.act_id,
                        principalTable: "act",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_plan_content_locality_locality_id",
                        column: x => x.locality_id,
                        principalTable: "locality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_plan_content_plan_plan_id",
                        column: x => x.plan_id,
                        principalTable: "plan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_act_contract_id",
                table: "act",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_act_executor_id",
                table: "act",
                column: "executor_id");

            migrationBuilder.CreateIndex(
                name: "IX_act_locality_id",
                table: "act",
                column: "locality_id");

            migrationBuilder.CreateIndex(
                name: "IX_act_file_parent_id",
                table: "act_file",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_animal_act_id",
                table: "animal",
                column: "act_id");

            migrationBuilder.CreateIndex(
                name: "IX_animal_file_parent_id",
                table: "animal_file",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_client_id",
                table: "contract",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_executor_id",
                table: "contract",
                column: "executor_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_number_client_id_executor_id_date_valid",
                table: "contract",
                columns: new[] { "number", "client_id", "executor_id", "date_valid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contract_content_contract_id",
                table: "contract_content",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_contract_content_locality_id_contract_id",
                table: "contract_content",
                columns: new[] { "locality_id", "contract_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contract_file_parent_id",
                table: "contract_file",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_entity_possibilities_role_id",
                table: "entity_possibilities",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_legal_type_name",
                table: "legal_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_locality_name",
                table: "locality",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_log_user_id",
                table: "log",
                column: "user_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_plan_status_id",
                table: "plan",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_plan_content_act_id",
                table: "plan_content",
                column: "act_id");

            migrationBuilder.CreateIndex(
                name: "IX_plan_content_locality_id",
                table: "plan_content",
                column: "locality_id");

            migrationBuilder.CreateIndex(
                name: "IX_plan_content_plan_id",
                table: "plan_content",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_status_id",
                table: "report",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_content_locality_id",
                table: "report_content",
                column: "locality_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_content_report_id",
                table: "report_content",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_name",
                table: "role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_type_organization_name",
                table: "type_organization",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_locality_id",
                table: "user",
                column: "locality_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                table: "user",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_organization_id",
                table: "user",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
            migrationBuilder.Sql(File.ReadAllText("Infrastructure\\Data\\InitData.sql"));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "act_file");

            migrationBuilder.DropTable(
                name: "animal_file");

            migrationBuilder.DropTable(
                name: "contract_content");

            migrationBuilder.DropTable(
                name: "contract_file");

            migrationBuilder.DropTable(
                name: "entity_possibilities");

            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "plan_content");

            migrationBuilder.DropTable(
                name: "report_content");

            migrationBuilder.DropTable(
                name: "animal");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "plan");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "act");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "plan_status");

            migrationBuilder.DropTable(
                name: "report_status");

            migrationBuilder.DropTable(
                name: "contract");

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
