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
			// Run the query, asking for A records and then for AAAA records.
			var result_v4 = await _client.QueryAsync(domain, QueryType.A);
			var result_v6 = await _client.QueryAsync(domain, QueryType.AAAA);

			// If there is an error in both queries, return an null pointer.
			if (result_v4.HasError && result_v6.HasError) { return null; }

			// Get the addresses from the returned records, and prepare the
			// IPAddress array for filling.
			var records_v4 = result_v4.Answers.AddressRecords().ToArray();
			var records_v6 = result_v6.Answers.AddressRecords().ToArray();
			var addresses = new List<IPAddress>();

			// Fill the IPAddress array with v4 and then v6 results.
			foreach (var record in records_v4)
			{
				addresses.Add(record.Address);
			}
			foreach (var record in records_v6)
			{
				addresses.Add(record.Address);
			}

			return addresses.ToArray();
		}
	}
}
