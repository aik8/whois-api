using System;
using HtmlAgilityPack;
using GrWhoisApi.Models;
using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using Microsoft.Extensions.Logging;
using GrWhoisApi.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Net;

namespace GrWhoisApi.Services
{
	public class PiosService : IPiosService
	{
		private readonly PiosServiceOptions _options;

		private readonly ILogger _logger;
		private UriBuilder _builder;


		public PiosService(
			IOptions<PiosServiceOptions> options,
			ILogger<PiosService> logger,
			IMemoryCache cache)
		{
			// Initialize the options.
			_options = options.Value;

			// Initialize injected stuff.
			_logger = logger;

			// Determine the scheme to be used.
			var scheme = _options.Secure ? "https" : "http";

			// Initialize the builder.
			_builder = new UriBuilder(scheme, _options.Host, _options.Port, _options.Path);
		}

		public IPiosResult AskPios(string domain)
		{
			// Log stuff.
			_logger.LogInformation($"Got request for {domain}");

			// Extract the base domain.
			var baseDomain = new BaseDomain(domain);

			// Check if a valid domain could be found, otherwise return an empty result.
			if (!baseDomain.IsValid)
			{
				_logger.LogWarning($"\"{domain}\" is an invalid .gr/.ελ domain.");
				return new PiosResult(domain);
			}

			// At this point it's clear we need to query The Registry.
			_logger.LogInformation($"No cached results found or fresh results were requested for {baseDomain}. Fetching some fresh information.");

			// Fill in the domain query.
			_builder.Query = $"domainName={baseDomain}";

			// Fetch what needs to be fetched.
			HtmlWeb web = new HtmlWeb();
			var htmlDoc = web.Load(_builder.Uri);

			// Isolate the response body.
			var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

			// Parse the result.
			var result = PiosResult.Parse(baseDomain.Value, node.InnerText);

			// We are done here.
			return result;
		}
	}
}
