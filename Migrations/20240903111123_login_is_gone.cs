using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortener_server.Migrations
{
    /// <inheritdoc />
    public partial class login_is_gone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "Users",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
