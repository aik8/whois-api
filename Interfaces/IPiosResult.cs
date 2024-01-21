using System.Collections.Generic;
using GrWhoisApi.Models;

namespace GrWhoisApi.Interfaces {

	/// <summary>
	/// Represents the processed results from a query to the registry.
	/// </summary>
	public interface IPiosResult {
		Domain Domain { get; }
		Registrar Registrar { get; }
		ICollection<NameServer> NameServers { get; }
		bool IsRegistered { get; }
	}
}
