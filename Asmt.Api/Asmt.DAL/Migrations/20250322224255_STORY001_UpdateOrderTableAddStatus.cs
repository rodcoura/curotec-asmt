using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asmt.DAL.Migrations
{
    /// <inheritdoc />
    public partial class STORY001UpdateOrderTableAddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 167, DateTimeKind.Utc).AddTicks(837));

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 167, DateTimeKind.Utc).AddTicks(1361));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(GETUTCDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 166, DateTimeKind.Utc).AddTicks(9623));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 167, DateTimeKind.Utc).AddTicks(837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 167, DateTimeKind.Utc).AddTicks(1361),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 21, 50, 52, 166, DateTimeKind.Utc).AddTicks(9623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(GETUTCDATE())");
        }
    }
}
