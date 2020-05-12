using System.Collections.Generic;
using KowWhoisApi.Models;
using KowWhoisApi.Interfaces;

namespace KowWhoisApi.Data
{
	public class PiosResult : IPiosResult
	{
		public Domain Domain { get; private set; }
		public Registrar Registrar { get; private set; }
		public ICollection<NameServer> NameServers { get; private set; }
		public bool IsRegistered { get; private set; }
		public bool IsCached { get; set; }

		public PiosResult() {}

		public PiosResult(string domain, bool registered = false, bool cached = false)
		{
			// Get the domain status of the result.
			IsRegistered = registered;

			// Let's start with "un-cached".
			IsCached = cached;

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
