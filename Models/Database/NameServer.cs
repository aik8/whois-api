using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GrWhoisApi.Models
{
	[Table("nameserver")]
	public class NameServer
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("name")]
		[Required]
		public string Name { get; set; }

		[Column("created_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		[JsonIgnore]
		public virtual ICollection<Snapshot> Snapshots { get; set; }
		[JsonIgnore]
		public virtual ICollection<SnapshotNameServer> SnapshotNameServers { get; set; }

		public ICollection<Address> Addresses { get; set; }
		[JsonIgnore]
		public virtual ICollection<NameServerAddress> NameServerAddresses { get; set; }

		public NameServer()
		{
			Snapshots = new List<Snapshot>();
			SnapshotNameServers = new List<SnapshotNameServer>();
			Addresses = new List<Address>();
			NameServerAddresses = new List<NameServerAddress>();
		}
	}
}
