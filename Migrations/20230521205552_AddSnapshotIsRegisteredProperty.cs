using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KowWhoisApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSnapshotIsRegisteredProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_registered",
                table: "snapshot",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_registered",
                table: "snapshot");
        }
    }
}
