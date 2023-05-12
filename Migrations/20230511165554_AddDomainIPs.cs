using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KowWhoisApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainIPs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rel_domain_address",
                columns: table => new
                {
                    domain_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    address_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_domain_address", x => new { x.domain_id, x.address_id });
                    table.ForeignKey(
                        name: "FK_rel_domain_address_address_address_id",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rel_domain_address_domain_domain_id",
                        column: x => x.domain_id,
                        principalTable: "domain",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_rel_domain_address_address_id",
                table: "rel_domain_address",
                column: "address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rel_domain_address");
        }
    }
}
