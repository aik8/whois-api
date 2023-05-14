using System;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KowWhoisApi.Services
{
	public class CacheService : ICacheService<Snapshot>
	{
		private readonly ILogger _logger;
		private readonly IMemoryCache _cache;
		private readonly MemoryCacheEntryOptions _entry_options;

		public CacheService(
			IOptions<PiosServiceOptions> options,
			ILogger<CacheService> logger,
			IMemoryCache cache
		)
		{
			_logger = logger;
			_cache = cache;

			var cache_ttl = options.Value.CacheTtl;
			var ttl = TimeSpan.FromSeconds(cache_ttl);
			_entry_options = new MemoryCacheEntryOptions()
				.SetAbsoluteExpiration(ttl);
		}

		public Snapshot Get(string key)
		{
			// If we find a previous result in the cache, marki it and return it.
			Snapshot value = new Snapshot();
			if (_cache.TryGetValue<Snapshot>(key, out value))
			{
				_logger.LogInformation($"Found {key} in cache.");
				value.IsCached = true;
			}

			// Return whatever the cache served.
			return value;
		}

		public void Set(string key, Snapshot value)
		{
			// Set the given key-value pair into the cache, with the configured TTL.
			_cache.Set<Snapshot>(key, value, _entry_options);
		}
	}
}
