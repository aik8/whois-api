using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KowWhoisApi.Models
{
	[Table("registrar")]
	public class Registrar
	{
		[Column("id")]
		public uint Id { get; set; }

		[Column("name")]
		[MaxLength(255)]
		[Required]
		public string Name { get; set; }

		[Column("url")]
		[MaxLength(255)]
		public string Url { get; set; }

		[Column("email")]
		[MaxLength(255)]
		public string Email { get; set; }

		[Column("phone")]
		[MaxLength(20)]
		public string Phone { get; set; }

		[Column("created_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; }

		[Column("updated_at", TypeName = "TIMESTAMP")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		[JsonIgnore]
		public virtual ICollection<Snapshot> Snapshots { get; set; }

		public Registrar()
		{
			Snapshots = new List<Snapshot>();
		}
	}
}
