using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KowWhoisApi.Migrations
{
    public partial class AddQueryLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registry_query",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    response = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registry_query", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "whois_query",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_ip_raw = table.Column<byte[]>(type: "varbinary(16)", maxLength: 16, nullable: false),
                    client_ip = table.Column<string>(type: "longtext", nullable: true, computedColumnSql: "INET6_NTOA(client_ip_raw)"),
                    client_hostname = table.Column<string>(type: "longtext", nullable: true),
                    response = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    registry_query_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_whois_query", x => x.id);
                    table.ForeignKey(
                        name: "FK_whois_query_registry_query_registry_query_id",
                        column: x => x.registry_query_id,
                        principalTable: "registry_query",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_whois_query_registry_query_id",
                table: "whois_query",
                column: "registry_query_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "whois_query");

            migrationBuilder.DropTable(
                name: "registry_query");
        }
    }
}
