using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KowWhoisApi.Models {
	[Table("rel_addressset_address")]
	public class AddressSetAddress {
		[Column("addressset_id")]
		[Required]
		public uint AddressSetId { get; set; }
		[JsonIgnore]
		public virtual AddressSet AddressSet { get; set; }

		[Column("address_id")]
		[Required]
		public uint AddressId { get; set; }
		public virtual Address Address { get; set; }
	}
}
