using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models
{
	[Table("whois_query")]
	public class WhoisQuery
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("client_ip_raw")]
		[MaxLength(16)]
		[Required]
		public byte[] ClientIpRaw { get; set; }

		[Column("client_ip")]
		public string ClientIp { get; set; }

		[Column("client_hostname")]
		public string ClientHostname { get; set; }

		[Column("response")]
		public ushort Response { get; set; }

		[Column("registry_query_id")]
		public uint? RegistryQueryId { get; set; }
		virtual public RegistryQuery RegistryQuery { get; set; }

		#region Timestamps

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		#endregion
	}
}
