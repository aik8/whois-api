using System;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Data
{
	public class WhoisContext : DbContext
	{
		public DbSet<Registrar> Registrars { get; set; }
		public DbSet<Domain> Domains { get; set; }
		public DbSet<NameServer> NameServers { get; set; }
		public DbSet<Snapshot> Snapshots { get; set; }
		public DbSet<SnapshotNameServer> NameServerSnapshots { get; set; }

		public WhoisContext(DbContextOptions<WhoisContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Domain
			modelBuilder.Entity<Domain>(entity =>
			{
				entity.HasIndex(e => e.Name).IsUnique();
				entity.HasIndex(e => e.Handle).IsUnique();
			});

			// Registrar
			modelBuilder.Entity<Registrar>().HasIndex(e => e.Name).IsUnique();

			// NameServer
			modelBuilder.Entity<NameServer>().HasIndex(e => e.Name).IsUnique();

			// NameServer - Snapshot Relationship
			modelBuilder.Entity<SnapshotNameServer>(entity =>
			{
				entity.HasKey(sns => new { sns.SnapshotId, sns.NameServerId });
			});
		}
	}
}
