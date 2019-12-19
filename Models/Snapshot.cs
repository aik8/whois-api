using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kow_whois_api
{
	[Table("snapshot")]
	public class Snapshot
	{
		[Column("id")]
		public int Id { get; set; }

		// Registrar
		[Column("registrar_id")]
		public int? RegistrarId { get; set; }
		public virtual Registrar Registrar { get; set; }

		// Domain
		[Column("domain_id")]
		[Required]
		public int DomainId { get; set; }
		public virtual Domain Domain { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		// NameServers
		public virtual List<NameServerSnapshot> NameServerSnapshots { get; set; }
	}
}
