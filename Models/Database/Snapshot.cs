using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KowWhoisApi.Models
{
	[Table("snapshot")]
	public class Snapshot
	{
		[Column("id")]
		public uint Id { get; set; }

		// Registrar
		[Column("registrar_id")]
		public uint? RegistrarId { get; set; }
		public virtual Registrar Registrar { get; set; }

		// Domain
		[Column("domain_id")]
		[Required]
		public uint DomainId { get; set; }
		public virtual Domain Domain { get; set; }

		[Column("created_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		// NameServers
		public virtual ICollection<SnapshotNameServer> SnapshotNameServers { get; set; }

		public Snapshot()
		{
			SnapshotNameServers = new List<SnapshotNameServer>();
		}
	}
}
