using System.Collections.Generic;
using KowWhoisApi.Models;
using KowWhoisApi.Interfaces;
using System;
using System.Globalization;

namespace KowWhoisApi.Data
{
	/// <summary>
	/// Implements the <see cref="IPiosResult"> interface in a simple way.
	/// </summary>
	public class PiosResult : IPiosResult
	{
		public Domain Domain { get; private set; }
		public Registrar Registrar { get; private set; }
		public ICollection<NameServer> NameServers { get; private set; }
		public bool IsRegistered { get; private set; }
		public bool IsCached { get; set; }

		public PiosResult() { }

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

		/// <summary>
		/// Parse the results returned from the Registry.
		/// </summary>
		/// <param name="domain">The domain to which the results refer.</param>
		/// <param name="raw_results">The raw registry query results.</param>
		/// <returns>A PiosResult object containing the parsed results.</returns>
		public static PiosResult Parse(string domain, string raw_results)
		{
			// Split the result into fields.
			var fields = raw_results.Split('\n');

			// Fill in the basics.
			var parsed = new PiosResult(domain, fields.Length > 2);

			// If it is not registered, just return what we got so far.
			if (!parsed.IsRegistered) return parsed;

			// OK, we got so far, so this domain is registered. Gather
			// all the key-value pairs into a List.
			var pairs = new List<string[]>();

			foreach (var field in fields)
			{
				// Split the pair.
				var pair = field.Split(':');

				// Get the key.
				var key = pair[0];

				// Initialize the value.
				var value = String.Empty;

				// The URL field gets split more than once, thus we need to
				// take extra care of the value field.
				if (pair.Length == 2)
				{
					value = pair[1];
				}
				else if (pair.Length == 3)
				{
					value = $"{pair[1]}:{pair[2]}";
				}

				// Check if there is anything in there.
				if (key.Length == 0) continue;

				// Add it to the pile of pairs.
				pairs.Add(new string[2] { key, value });
			}

			// For this first version we assume that the order of fields
			// is always the same in the response.
			var cult = new CultureInfo("el-GR");

			/* Domain */
			parsed.Domain.Handle = pairs[1][1];
			parsed.Domain.ProtocolNumber = pairs[2][1];
			parsed.Domain.CreationDate = DateTime.Parse(pairs[3][1], cult);
			parsed.Domain.ExpirationDate = DateTime.Parse(pairs[4][1], cult);
			parsed.Domain.LastUpdate = DateTime.Parse(pairs[5][1], cult);

			/* Current Registrar */
			parsed.Registrar.Name = pairs[6][1];
			parsed.Registrar.Url = pairs[7][1];
			parsed.Registrar.Email = pairs[8][1];
			parsed.Registrar.Phone = pairs[9][1];

			/* Name Servers */
			for (var i = 12; i < pairs.Count; i++)
			{
				// Create the name server.
				var ns = new NameServer();

				// Fill it.
				ns.Name = pairs[i][1];

				// Add it to the list.
				parsed.NameServers.Add(ns);
			}

			// We are done here.
			return parsed;
		}
	}
}
