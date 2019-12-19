using System.ComponentModel.DataAnnotations.Schema;

namespace kow_whois_api
{
	[Table("rel_nameserver_snapshot")]
	public class NameServerSnapshot
	{
		public int NameServerId { get; set; }
		public NameServer NameServer { get; set; }

		public int SnapshotId { get; set; }
		public Snapshot Snapshot { get; set; }
	}
}
