using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asmt.DAL.Migrations
{
    /// <inheritdoc />
    public partial class STORY001SeedCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Name" },
                values: new object[] { "John Doe" }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Name" },
                values: new object[] { "Jane Doe" }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Name" },
                values: new object[] { "Jim Doe" }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Name" },
                values: new object[] { "Jill Doe" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Name",
                keyValue: "John Doe"
            );

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Name",
                keyValue: "Jane Doe"
            );

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Name",
                keyValue: "Jim Doe"
            );

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Name",
                keyValue: "Jill Doe"
            );
        }
    }
}
