using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models
{
	[Table("domain")]
	public class Domain
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("name")]
		[Required]
		public string Name { get; set; }

		[Column("handle")]
		public string Handle { get; set; }

		[Column("protonum")]
		[MaxLength(128)]
		public string ProtocolNumber { get; set; }

		[Column("creation")]
		public DateTime? CreationDate { get; set; }

		[Column("expiration")]
		public DateTime? ExpirationDate { get; set; }

		[Column("last_update")]
		public DateTime? LastUpdate { get; set; }

		[Column("created_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		[JsonIgnore]
		public virtual ICollection<Snapshot> Snapshots { get; set; }

		public Domain()
		{
			Snapshots = new List<Snapshot>();
		}
	}
}
