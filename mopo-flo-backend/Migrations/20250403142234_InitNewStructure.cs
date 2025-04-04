using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace violet.backend.Migrations
{
    /// <inheritdoc />
    public partial class InitNewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AggregateType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DomainEventType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EventData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 3, 17, 52, 34, 525, DateTimeKind.Local).AddTicks(1010))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InitialSetup = table.Column<bool>(type: "bit", nullable: false),
                    TelegramInfo_TelegramId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramInfo_IsBot = table.Column<bool>(type: "bit", nullable: true),
                    TelegramInfo_FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TelegramInfo_LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TelegramInfo_Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TelegramInfo_LanguageCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TelegramInfo_IsPremium = table.Column<bool>(type: "bit", nullable: true),
                    TelegramInfo_AddedToAttachmentMenu = table.Column<bool>(type: "bit", nullable: true),
                    TelegramInfo_AllowsWriteToPm = table.Column<bool>(type: "bit", nullable: true),
                    TelegramInfo_PhotoUrl = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    TelegramInfo_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FemaleUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FemaleProfile_IsNewInPeriod = table.Column<bool>(type: "bit", nullable: true),
                    FemaleProfile_PeriodCycleDuration = table.Column<int>(type: "int", nullable: true),
                    FemaleProfile_BleedingDuration = table.Column<int>(type: "int", nullable: true),
                    FemaleProfile_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FemaleProfile_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FemaleProfile_Age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FemaleUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FemaleUsers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaleUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaleProfile_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaleProfile_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaleProfile_Age = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaleUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaleUsers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDayOfPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDayOfBleeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FemaleUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periods_FemaleUsers_FemaleUserId",
                        column: x => x.FemaleUserId,
                        principalTable: "FemaleUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Periods_FemaleUserId",
                table: "Periods",
                column: "FemaleUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "MaleUsers");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "FemaleUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
