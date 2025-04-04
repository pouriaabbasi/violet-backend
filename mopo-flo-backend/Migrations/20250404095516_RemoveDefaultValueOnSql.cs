using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace violet.backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDefaultValueOnSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 4, 13, 22, 3, 813, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.AddForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods",
                column: "FemaleUserId",
                principalTable: "FemaleUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 4, 13, 22, 3, 813, DateTimeKind.Local).AddTicks(9133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods",
                column: "FemaleUserId",
                principalTable: "FemaleUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
