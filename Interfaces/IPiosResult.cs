using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi {
	public interface IPiosResult {
		Domain Domain { get; set; }
		Registrar Registrar { get; set; }
		ICollection<NameServer> NameServers { get; set; }
		bool IsRegistered { get; set; }
	}
}
