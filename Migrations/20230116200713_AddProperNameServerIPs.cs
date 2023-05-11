using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KowWhoisApi.Migrations
{
    public partial class AddProperNameServerIPs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_rel_snapshot_nameserver_snapshot_id_nameserver_id",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_address_snapshot_id_nameserver_id",
                table: "address");

            migrationBuilder.DropColumn(
                name: "nameserver_id",
                table: "address");

            migrationBuilder.DropColumn(
                name: "snapshot_id",
                table: "address");

            migrationBuilder.CreateTable(
                name: "rel_nameserver_address",
                columns: table => new
                {
                    nameserver_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    address_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_nameserver_address", x => new { x.nameserver_id, x.address_id });
                    table.ForeignKey(
                        name: "FK_rel_nameserver_address_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rel_nameserver_address_nameserver_nameserver_id",
                        column: x => x.nameserver_id,
                        principalTable: "nameserver",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nameserver_address_address_id",
                table: "rel_nameserver_address",
                column: "address_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rel_nameserver_address");

            migrationBuilder.AddColumn<uint>(
                name: "nameserver_id",
                table: "address",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "snapshot_id",
                table: "address",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_address_snapshot_id_nameserver_id",
                table: "address",
                columns: new[] { "snapshot_id", "nameserver_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_address_rel_snapshot_nameserver_snapshot_id_nameserver_id",
                table: "address",
                columns: new[] { "snapshot_id", "nameserver_id" },
                principalTable: "rel_snapshot_nameserver",
                principalColumns: new[] { "snapshot_id", "nameserver_id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
