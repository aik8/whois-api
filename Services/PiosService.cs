using System;
using System.Collections.Generic;
using System.Globalization;
using HtmlAgilityPack;
using KowWhoisApi.Models;
using KowWhoisApi.Data;

namespace KowWhoisApi.Services
{
	public class PiosService : IPiosService
	{
		private string _scheme = "https";
		private string _host = @"grwhois.ics.forth.gr";
		private int _port = 800;
		private string _path = @"plainwhois/plainWhois";
		private UriBuilder _builder;

		public PiosService()
		{
			// Initialize the builder.
			_builder = new UriBuilder(_scheme, _host, _port, _path);
		}

		public IPiosResult AskPios(string domain)
		{
			// Fill in the domain query.
			_builder.Query = $"domainName={domain}";

			// Fetch what needs to be fetched.
			HtmlWeb web = new HtmlWeb();
			var htmlDoc = web.Load(_builder.Uri);

			// Isolate the response body.
			var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

			// Parse the result.
			var result = ParsePios(domain, node.InnerText);

			// We are done here.
			return result;
		}

		private IPiosResult ParsePios(string domain, string result)
		{
			// Split the result into fields.
			var fields = result.Split('\n');

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

				// Get the kay and value.
				var key = pair[0];
				var value = pair.Length == 2 ? pair[1] : String.Empty;

				// Check if there is anything in there.
				if (key.Length == 0) continue;

				// Add it to the pile of pairs.
				pairs.Add(new string[2] { key, value });
			}

			// For this first version we assume that the order of fields
			// is always the same in the response.
			var cult = new CultureInfo("el-GR");

			/* Domain */
			parsed.Domain.Handle         = pairs[1][1];
			parsed.Domain.ProtocolNumber = pairs[2][1];
			parsed.Domain.CreationDate   = DateTime.Parse(pairs[3][1], cult);
			parsed.Domain.ExpirationDate = DateTime.Parse(pairs[4][1], cult);
			parsed.Domain.LastUpdate     = DateTime.Parse(pairs[5][1], cult);

			/* Current Registrar */
			parsed.Registrar.Name  = pairs[6][1];
			parsed.Registrar.Url   = pairs[7][1];
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
