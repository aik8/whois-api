using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Data
{
	public class PiosResult : IPiosResult
	{
		public Domain Domain { get; private set; }
		public Registrar Registrar { get; private set; }
		public ICollection<NameServer> NameServers { get; private set; }
		public bool IsRegistered { get; private set; }

		public PiosResult(string domain, bool registered)
		{
			// Get the domain status of the result.
			IsRegistered = registered;

			// Fill in the domain name.
			Domain = new Domain { Name = domain };

			// If the domain is not registered, skip the rest.
			if (!IsRegistered) return;

			// There is more to be added here.
			Registrar = new Registrar();
			NameServers = new List<NameServer>();
		}
	}
}
