using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asmt.DAL.Migrations
{
    /// <inheritdoc />
    public partial class STORY002SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { 
                    "Name",
                    "Email",
                    "Password",
                    "Role",
                },
                values: new object[] { 
                    "John User",
                    "john.user@curotec.com",
                    "12345678",
                    "Admin",
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Name",
                keyValue: "John User"
            );
        }
    }
}
