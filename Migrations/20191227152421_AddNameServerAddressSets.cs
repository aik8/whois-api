using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KowWhoisApi.Migrations
{
	public partial class AddNameServerAddressSets : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "address",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					ip = table.Column<byte[]>(maxLength: 16, nullable: false),
					addr = table.Column<string>(maxLength: 45, nullable: false, computedColumnSql: "INET6_NTOA(ip)"),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_address", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "addressset",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
					nameserver_id = table.Column<uint>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_addressset", x => x.id);
					table.ForeignKey(
						name: "FK_addressset_nameserver_nameserver_id",
						column: x => x.nameserver_id,
						principalTable: "nameserver",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "rel_addressset_address",
				columns: table => new
				{
					addressset_id = table.Column<uint>(nullable: false),
					address_id = table.Column<uint>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_rel_addressset_address", x => new { x.addressset_id, x.address_id });
					table.ForeignKey(
						name: "FK_rel_addressset_address_address_address_id",
						column: x => x.address_id,
						principalTable: "address",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_rel_addressset_address_addressset_addressset_id",
						column: x => x.addressset_id,
						principalTable: "addressset",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_address_ip",
				table: "address",
				column: "ip",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_addressset_nameserver_id",
				table: "addressset",
				column: "nameserver_id");

			migrationBuilder.CreateIndex(
				name: "IX_rel_addressset_address_address_id",
				table: "rel_addressset_address",
				column: "address_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "rel_addressset_address");

			migrationBuilder.DropTable(
				name: "address");

			migrationBuilder.DropTable(
				name: "addressset");
		}
	}
}
