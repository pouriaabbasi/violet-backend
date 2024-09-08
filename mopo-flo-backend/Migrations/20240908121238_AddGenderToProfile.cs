using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mopo_flo_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Profiles");
        }
    }
}
