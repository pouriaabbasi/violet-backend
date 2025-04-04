using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace violet.backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProfileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Periods_FemaleUsers_FemaleUserId",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "TelegramInfo_CreateDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MaleProfile_CreateDate",
                table: "MaleUsers");

            migrationBuilder.DropColumn(
                name: "FemaleProfile_CreateDate",
                table: "FemaleUsers");

            migrationBuilder.RenameColumn(
                name: "MaleProfile_Age",
                table: "MaleUsers",
                newName: "MaleProfile_Height");

            migrationBuilder.RenameColumn(
                name: "FemaleProfile_Age",
                table: "FemaleUsers",
                newName: "FemaleProfile_Height");

            migrationBuilder.AddColumn<int>(
                name: "MaleProfile_BirthYear",
                table: "MaleUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaleProfile_Weigh",
                table: "MaleUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FemaleProfile_BirthYear",
                table: "FemaleUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FemaleProfile_Weigh",
                table: "FemaleUsers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 3, 22, 34, 17, 446, DateTimeKind.Local).AddTicks(759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 3, 17, 52, 34, 525, DateTimeKind.Local).AddTicks(1010));

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

            migrationBuilder.DropColumn(
                name: "MaleProfile_BirthYear",
                table: "MaleUsers");

            migrationBuilder.DropColumn(
                name: "MaleProfile_Weigh",
                table: "MaleUsers");

            migrationBuilder.DropColumn(
                name: "FemaleProfile_BirthYear",
                table: "FemaleUsers");

            migrationBuilder.DropColumn(
                name: "FemaleProfile_Weigh",
                table: "FemaleUsers");

            migrationBuilder.RenameColumn(
                name: "MaleProfile_Height",
                table: "MaleUsers",
                newName: "MaleProfile_Age");

            migrationBuilder.RenameColumn(
                name: "FemaleProfile_Height",
                table: "FemaleUsers",
                newName: "FemaleProfile_Age");

            migrationBuilder.AddColumn<DateTime>(
                name: "TelegramInfo_CreateDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaleProfile_CreateDate",
                table: "MaleUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FemaleProfile_CreateDate",
                table: "FemaleUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 3, 17, 52, 34, 525, DateTimeKind.Local).AddTicks(1010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 3, 22, 34, 17, 446, DateTimeKind.Local).AddTicks(759));

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
