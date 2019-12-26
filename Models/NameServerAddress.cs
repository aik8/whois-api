using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models {
	[Table("nameserver_address")]
	public class NameServerAddress {
		[Column("id")]
		public uint Id { get; set; }

		[Column("ip")]
		[Required]
		public uint Ip { get; set; }

		[Column("address")]
		[MaxLength(16)]
		[Required]
		public string Address { get; set; }

		[Column("nameserver_id")]
		[Required]
		public uint NameServerId { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }
	}
}
