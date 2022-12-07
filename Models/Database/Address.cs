using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KowWhoisApi.Models
{
	[Table("address")]
	public class Address
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("ip_raw")]
		[MaxLength(16)]
		[Required]
		public byte[] IpRaw { get; set; }

		[Column("ip")]
		public string Ip { get; set; }

		[Column("snapshot_id")]
		public uint SnapshotNameServerSnapshotId { get; set; }
		[Column("nameserver_id")]
		public uint SnapshotNameServerNameServerId { get; set; }
		public virtual SnapshotNameServer SnapshotNameServer { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }
	}
}
