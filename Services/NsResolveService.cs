using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DnsClient;
using KowWhoisApi.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace KowWhoisApi.Services
{
	public class NsResolveService : INsResolveSerivce
	{
		private LookupClient _client;
		public NsResolveService()
		{
			var options = new LookupClientOptions()
			{
				UseCache = true
			};

			_client = new LookupClient(options);
		}

		/// <summary>
		/// Resolves the provided domain, asking for ANY records.
		/// </summary>
		/// <param name="domain">The domain to be resolved.</param>
		/// <returns>An array of IPAddress, containing both IPv4 and IPv6 entries.</returns>
		public async Task<IPAddress[]> Resolve(string domain)
		{
			// Run the query, asking for ANY.
			var result = await _client.QueryAsync(domain, QueryType.ANY);

			// If there is an error, return an null pointer.
			if (result.HasError) { return null; }

			// Get the addresses from the returned records, and prepare the
			// IPAddress array for filling.
			var records = result.Answers.AddressRecords().ToArray();
			var addresses = new List<IPAddress>();

			// Fill the IPAddress array.
			foreach (var record in records)
			{
				addresses.Add(record.Address);
			}

			return addresses.ToArray();
		}
	}
}
