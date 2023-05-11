using System;
using HtmlAgilityPack;
using KowWhoisApi.Models;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using Microsoft.Extensions.Logging;
using KowWhoisApi.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Net;

namespace KowWhoisApi.Services
{
	public class PiosService : IPiosService
	{
		private readonly PiosServiceOptions _options;

		private readonly int _cache_ttl;
		private readonly ILogger _logger;
		private readonly IMemoryCache _cache;
		private readonly INsResolveSerivce _resolver;
		private UriBuilder _builder;


		public PiosService(
			IOptions<PiosServiceOptions> options,
			ILogger<PiosService> logger,
			IMemoryCache cache,
			INsResolveSerivce resolver)
		{
			// Initialize the options.
			_options = options.Value;

			// Initialize injected stuff.
			_logger = logger;
			_cache = cache;
			_resolver = resolver;

			// Save the cache TTL.
			_cache_ttl = _options.CacheTtl;

			// Determine the scheme to be used.
			var scheme = _options.Secure ? "https" : "http";

			// Initialize the builder.
			_builder = new UriBuilder(scheme, _options.Host, _options.Port, _options.Path);
		}

		public async Task<IPiosResult> AskPios(string domain, bool fresh = false)
		{
			// Log stuff.
			_logger.LogInformation($"Got request for {domain}");

			// Extract the base domain.
			var baseDomain = new BaseDomain(domain);

			// Check if a valid domain could be found, otherwise return an empty result.
			if (!baseDomain.IsValid)
			{
				_logger.LogInformation($"\"{domain}\" is an invalid .gr/.ελ domain.");
				return new PiosResult();
			}

			// Check for cached results, unless otherwise requested.
			if (!fresh)
			{
				var cached = RecallResult(baseDomain.Value);
				if (cached != null)
				{
					return cached;
				}
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

			// Resolve the addresses of the nameservers.
			foreach (var ns in result.NameServers)
			{
				var addresses = await _resolver.Resolve(ns.Name);
				foreach (var address in addresses) {
					ns.Addresses.Add(new Address(address));
				}
			}

			// Cache the result.
			CacheResult(result);

			// We are done here.
			return result;
		}

		/// <summary>
		/// Writes results to the cache.
		/// </summary>
		/// <param name="result">The result to be cached.</param>
		private void CacheResult(PiosResult result)
		{
			// Cache the result.
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.CacheTtl));
			_cache.Set<PiosResult>(result.Domain.Name, result, cacheOptions);
		}

		/// <summary>
		/// Tries to recall the results for a given domain from the cache.
		/// </summary>
		/// <param name="domain">The domain for which to check.</param>
		/// <returns>The <see cref="PiosResult"> for the domain (marked as "cached") or null.</returns>
		private PiosResult RecallResult(string domain)
		{
			// If we find a previous result in the cache, mark it and return it.
			PiosResult cached = new PiosResult();
			if (_cache.TryGetValue<PiosResult>(domain, out cached))
			{
				_logger.LogInformation($"Found {domain} in cache.");
				cached.IsCached = true;
			}

			// Return whatever the cache served.
			return cached;
		}
	}
}
