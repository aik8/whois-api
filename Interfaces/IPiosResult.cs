using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces {

	/// <summary>
	/// Represents the processed results from a query to the .gr registry.
	/// </summary>
	public interface IPiosResult {
		Domain Domain { get; }
		Registrar Registrar { get; }
		ICollection<NameServer> NameServers { get; }
		bool IsRegistered { get; }
		bool IsCached { get; }
	}
}
