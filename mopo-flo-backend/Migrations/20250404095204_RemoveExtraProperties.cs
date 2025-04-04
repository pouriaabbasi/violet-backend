using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace violet.backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "FemaleProfile_IsNewInPeriod",
                table: "FemaleUsers");

            migrationBuilder.DropColumn(
                name: "AggregateType",
                table: "Events");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 4, 13, 22, 3, 813, DateTimeKind.Local).AddTicks(9133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 3, 22, 36, 54, 696, DateTimeKind.Local).AddTicks(2594));

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

            migrationBuilder.AddColumn<bool>(
                name: "FemaleProfile_IsNewInPeriod",
                table: "FemaleUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 3, 22, 36, 54, 696, DateTimeKind.Local).AddTicks(2594),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 4, 13, 22, 3, 813, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.AddColumn<string>(
                name: "AggregateType",
                table: "Events",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
