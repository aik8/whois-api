using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kow_whois_api.Migrations
{
	public partial class BigBang : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "domain",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(nullable: false),
					handle = table.Column<string>(nullable: true),
					protonum = table.Column<string>(maxLength: 128, nullable: true),
					creation = table.Column<DateTime>(nullable: true),
					expiration = table.Column<DateTime>(nullable: true),
					last_update = table.Column<DateTime>(nullable: true),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_domain", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "nameserver",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(nullable: false),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_nameserver", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "registrar",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(maxLength: 255, nullable: false),
					url = table.Column<string>(maxLength: 255, nullable: true),
					email = table.Column<string>(maxLength: 255, nullable: true),
					phone = table.Column<string>(maxLength: 20, nullable: true),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_registrar", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "snapshot",
				columns: table => new
				{
					id = table.Column<uint>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					registrar_id = table.Column<uint>(nullable: true),
					domain_id = table.Column<uint>(nullable: false),
					created_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					updated_at = table.Column<DateTime>(nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_snapshot", x => x.id);
					table.ForeignKey(
						name: "FK_snapshot_domain_domain_id",
						column: x => x.domain_id,
						principalTable: "domain",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_snapshot_registrar_registrar_id",
						column: x => x.registrar_id,
						principalTable: "registrar",
						principalColumn: "id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "rel_snapshot_nameserver",
				columns: table => new
				{
					snapshot_id = table.Column<uint>(nullable: false),
					nameserver_id = table.Column<uint>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_rel_snapshot_nameserver", x => new { x.snapshot_id, x.nameserver_id });
					table.ForeignKey(
						name: "FK_rel_snapshot_nameserver_nameserver_nameserver_id",
						column: x => x.nameserver_id,
						principalTable: "nameserver",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_rel_snapshot_nameserver_snapshot_snapshot_id",
						column: x => x.snapshot_id,
						principalTable: "snapshot",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_domain_handle",
				table: "domain",
				column: "handle",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_domain_name",
				table: "domain",
				column: "name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_nameserver_name",
				table: "nameserver",
				column: "name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_registrar_name",
				table: "registrar",
				column: "name",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_rel_snapshot_nameserver_nameserver_id",
				table: "rel_snapshot_nameserver",
				column: "nameserver_id");

			migrationBuilder.CreateIndex(
				name: "IX_snapshot_domain_id",
				table: "snapshot",
				column: "domain_id");

			migrationBuilder.CreateIndex(
				name: "IX_snapshot_registrar_id",
				table: "snapshot",
				column: "registrar_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "rel_snapshot_nameserver");

			migrationBuilder.DropTable(
				name: "nameserver");

			migrationBuilder.DropTable(
				name: "snapshot");

			migrationBuilder.DropTable(
				name: "domain");

			migrationBuilder.DropTable(
				name: "registrar");
		}
	}
}
