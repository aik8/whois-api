using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KowWhoisApi.Models
{
	[Table("rel_snapshot_nameserver")]
	public class SnapshotNameServer
	{
		[Column("snapshot_id")]
		[Required]
		public uint SnapshotId { get; set; }
		[JsonIgnore]
		public virtual Snapshot Snapshot { get; set; }

		[Column("nameserver_id")]
		[Required]
		public uint NameServerId { get; set; }
		public virtual NameServer NameServer { get; set; }
	}
}
