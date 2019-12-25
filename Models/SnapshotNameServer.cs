using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models
{
	[Table("rel_snapshot_nameserver")]
	public class SnapshotNameServer
	{
		[Column("snapshot_id")]
		[Required]
		public uint SnapshotId { get; set; }
		[JsonIgnore]
		public Snapshot Snapshot { get; set; }

		[Column("nameserver_id")]
		[Required]
		public uint NameServerId { get; set; }
		public NameServer NameServer { get; set; }
	}
}
