using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mopo_flo_backend.Migrations
{
    /// <inheritdoc />
    public partial class changeProfileTable_addBleedingDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BleedingDuration",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BleedingDuration",
                table: "Profiles");
        }
    }
}
