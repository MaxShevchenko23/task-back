using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortener_server.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    user_role_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.user_role_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    user_role_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_user_role_id",
                        column: x => x.user_role_id,
                        principalTable: "UserRoles",
                        principalColumn: "user_role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    link_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    source = table.Column<string>(type: "text", nullable: false),
                    shortened = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    views = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.link_id);
                    table.ForeignKey(
                        name: "FK_Links_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_user_id",
                table: "Links",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_user_role_id",
                table: "Users",
                column: "user_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
