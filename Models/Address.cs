using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models {
	[Table("address")]
	public class Address {
		[Column("id")]
		public uint Id { get; set; }

		[Column("ip")]
		[MaxLength(16)]
		[Required]
		public byte[] Ip { get; set; }

		[Column("addr")]
		[MaxLength(45)]
		[Required]
		public string Addr { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }
	}
}
