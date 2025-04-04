using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace violet.backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProfileEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaleProfile_Weigh",
                table: "MaleUsers",
                type: "DECIMAL(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FemaleProfile_Weigh",
                table: "FemaleUsers",
                type: "DECIMAL(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 3, 22, 36, 54, 696, DateTimeKind.Local).AddTicks(2594),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 3, 22, 34, 17, 446, DateTimeKind.Local).AddTicks(759));

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

            migrationBuilder.AlterColumn<decimal>(
                name: "MaleProfile_Weigh",
                table: "MaleUsers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FemaleProfile_Weigh",
                table: "FemaleUsers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 3, 22, 34, 17, 446, DateTimeKind.Local).AddTicks(759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 3, 22, 36, 54, 696, DateTimeKind.Local).AddTicks(2594));

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
