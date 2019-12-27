using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KowWhoisApi.Models {
	[Table("addressset")]
	public class AddressSet {
		[Column("id")]
		public uint Id { get; set; }

		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		[Column("nameserver_id")]
		[Required]
		public uint NameServerId { get; set; }
	}
}
