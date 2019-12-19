using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace kow_whois_api
{
	class WhoisContext : DbContext
	{
		public DbSet<Registrar> Registrars { get; set; }
		public DbSet<Domain> Domains { get; set; }
		public DbSet<NameServer> NameServers { get; set; }
		public DbSet<Snapshot> Snapshots { get; set; }
		public DbSet<NameServerSnapshot> NameServerSnapshots { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseMySql("server=localhost;database=whois;username=kootoor;password=funkybudha", mysqlOptions
				=> mysqlOptions.CharSet(CharSet.Utf8Mb4));

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Domain
			modelBuilder.Entity<Domain>(entity => {
				entity.HasIndex(e => e.Name).IsUnique();
				entity.HasIndex(e => e.Handle).IsUnique();
			});

			// Registrar
			modelBuilder.Entity<Registrar>().HasIndex(e => e.Name).IsUnique();

			// NameServer
			modelBuilder.Entity<NameServer>().HasIndex(e => e.Name).IsUnique();

			// NameServer - Snapshot Relationship
			modelBuilder.Entity<NameServerSnapshot>()
				.HasKey(nss => new { nss.NameServerId, nss.SnapshotId });
			modelBuilder.Entity<NameServerSnapshot>()
				.HasOne(nss => nss.NameServer)
				.WithMany(nss => nss.NameServerSnapshots)
				.HasForeignKey(nss => nss.NameServerId);
			modelBuilder.Entity<NameServerSnapshot>()
				.HasOne(nss => nss.Snapshot)
				.WithMany(nss => nss.NameServerSnapshots)
				.HasForeignKey(nss => nss.SnapshotId);
		}

	}
}
