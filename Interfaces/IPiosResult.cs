using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces {
	public interface IPiosResult {
		Domain Domain { get; }
		Registrar Registrar { get; }
		ICollection<NameServer> NameServers { get; }
		bool IsRegistered { get; }
		bool IsCached { get; }
	}
}
