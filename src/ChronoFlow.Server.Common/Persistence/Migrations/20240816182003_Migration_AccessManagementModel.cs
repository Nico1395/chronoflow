using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChronoFlow.Server.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration_AccessManagementModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "access_management_employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    personnel_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_changed = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    address_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address_country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address_house_number = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    address_postal_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    address_state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address_street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_management_permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_management_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_changed = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "access_management_employee_emails",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    fk_access_management_employee_emails = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_employee_emails", x => new { x.employee_id, x.email });
                    table.ForeignKey(
                        name: "FK_access_management_employee_emails_access_management_employe~",
                        column: x => x.fk_access_management_employee_emails,
                        principalTable: "access_management_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_management_employee_phone_numbers",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fk_access_management_employee_phone_numbers = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_employee_phone_numbers", x => new { x.employee_id, x.phone_number });
                    table.ForeignKey(
                        name: "FK_access_management_employee_phone_numbers_access_management_~",
                        column: x => x.fk_access_management_employee_phone_numbers,
                        principalTable: "access_management_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_management_employee_roles",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_employee_roles", x => new { x.employee_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_access_management_employee_roles_access_management_employee~",
                        column: x => x.employee_id,
                        principalTable: "access_management_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_access_management_employee_roles_access_management_roles_ro~",
                        column: x => x.role_id,
                        principalTable: "access_management_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_management_role_permissions",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_management_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_access_management_role_permissions_access_management_permis~",
                        column: x => x.permission_id,
                        principalTable: "access_management_permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_access_management_role_permissions_access_management_roles_~",
                        column: x => x.role_id,
                        principalTable: "access_management_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_management_employee_emails_email",
                table: "access_management_employee_emails",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_access_management_employee_emails_fk_access_management_empl~",
                table: "access_management_employee_emails",
                column: "fk_access_management_employee_emails");

            migrationBuilder.CreateIndex(
                name: "IX_access_management_employee_phone_numbers_fk_access_manageme~",
                table: "access_management_employee_phone_numbers",
                column: "fk_access_management_employee_phone_numbers");

            migrationBuilder.CreateIndex(
                name: "IX_access_management_employee_roles_role_id",
                table: "access_management_employee_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_management_employees_personnel_number",
                table: "access_management_employees",
                column: "personnel_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_access_management_permissions_name",
                table: "access_management_permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_access_management_role_permissions_permission_id",
                table: "access_management_role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_management_roles_name",
                table: "access_management_roles",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_management_employee_emails");

            migrationBuilder.DropTable(
                name: "access_management_employee_phone_numbers");

            migrationBuilder.DropTable(
                name: "access_management_employee_roles");

            migrationBuilder.DropTable(
                name: "access_management_role_permissions");

            migrationBuilder.DropTable(
                name: "access_management_employees");

            migrationBuilder.DropTable(
                name: "access_management_permissions");

            migrationBuilder.DropTable(
                name: "access_management_roles");
        }
    }
}
