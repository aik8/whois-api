using System;
using HtmlAgilityPack;
using GrWhoisApi.Models;
using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using Microsoft.Extensions.Logging;
using GrWhoisApi.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

namespace GrWhoisApi.Services
{
	public class PiosService : IPiosService
	{
		private readonly QueryServiceOptions _options;

		private readonly ILogger _logger;
		private readonly UriBuilder _builder;
		private readonly HtmlWeb _web;


		public PiosService(
			IOptions<QueryServiceOptions> options,
			ILogger<PiosService> logger,
			IMemoryCache cache,
			HtmlWeb web)
		{
			// Initialize the options.
			_options = options.Value;

			// Initialize injected stuff.
			_logger = logger;
			_web = web;

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

			// Prepare the result variable.
			PiosResult result;

			try
			{
				// Fetch the resutls from the registry.
				var htmlDoc = _web.Load(_builder.Uri);

				// Isolate the response body.
				var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

				// Parse the result.
				result = PiosResult.Parse(baseDomain.Value, node.InnerText);
			}
			catch (Exception ex)
			{
				// Log the error.
				_logger.LogError(ex, $"An error occured while fetching results for {baseDomain}.");

				// Return an empty result.
				result = new PiosResult(domain);
			}

			// Return the result.
			return result;
		}
	}
}
