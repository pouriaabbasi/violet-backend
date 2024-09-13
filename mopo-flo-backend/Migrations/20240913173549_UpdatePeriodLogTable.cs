using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mopo_flo_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePeriodLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDayOfBleeding",
                table: "PeriodLogs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDayOfBleeding",
                table: "PeriodLogs");
        }
    }
}
