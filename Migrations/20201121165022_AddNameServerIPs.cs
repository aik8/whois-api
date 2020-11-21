using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KowWhoisApi.Migrations
{
    public partial class AddNameServerIPs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ip_raw = table.Column<byte[]>(type: "varbinary(16)", maxLength: 16, nullable: false),
                    ip = table.Column<string>(type: "longtext", nullable: true, computedColumnSql: "INET6_NTOA(ip_raw)"),
                    snapshot_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    nameserver_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_rel_snapshot_nameserver_snapshot_id_nameserver_id",
                        columns: x => new { x.snapshot_id, x.nameserver_id },
                        principalTable: "rel_snapshot_nameserver",
                        principalColumns: new[] { "snapshot_id", "nameserver_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_ip_raw",
                table: "address",
                column: "ip_raw",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_address_snapshot_id_nameserver_id",
                table: "address",
                columns: new[] { "snapshot_id", "nameserver_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
