using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kow_whois_api
{
	[Table("nameserver")]
	public class NameServer
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("name")]
		[Required]
		public string Name { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<SnapshotNameServer> SnapshotNameServers { get; set; }

		public NameServer()
		{
			SnapshotNameServers = new List<SnapshotNameServer>();
		}
	}
}