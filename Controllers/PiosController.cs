using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrWhoisApi.Interfaces;
using GrWhoisApi.Models;

namespace GrWhoisApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PiosController : ControllerBase
	{
		private readonly IPiosService _pios;
		private readonly ISnapshotsService _snapshots;
		private readonly ICacheService<Snapshot> _cache;

		public PiosController(
			IPiosService pios,
			ISnapshotsService snapshots,
			ICacheService<Snapshot> cache
			)
		{
			_pios = pios;
			_snapshots = snapshots;
			_cache = cache;
		}

		[HttpGet]
		public async Task<ActionResult<IPiosResult>> Get([FromQuery] string domain, [FromQuery] bool fast = false, [FromQuery] bool fresh = false)
		{
			// Check if we got a valid query.
			if (domain == null)
			{
				return BadRequest();
			}

			// If this request comes from an automated system,
			// there is no need for it to wait for a response.
			if (fast)
			{
				// Start the whole data collection chain...
				Response.OnCompleted(() => Ask(domain, fresh));

				// ... and send immediately just a 202 to the requester.
				return Accepted();
			}

			// Wait for the result.
			var result = await Ask(domain, fresh);

			// Otherwise do return something.
			return Ok(result);
		}

		private Task<Snapshot> Ask(string domain, bool fresh)
		{
			return Task.Factory.StartNew(() =>
			{
				Snapshot snapshot = null;

				// If a fresh result is not explicitly requested, start by getting
				// what we have in cache for the requested domain.
				if (!fresh) snapshot = _cache.Get(domain);

				// If the cache returned something, we are done. Otherwise, continue.
				if (snapshot != null) return snapshot;

				// Fetch nice, clean results.
				var pios_result = _pios.AskPios(domain);

				// Create a snapshot out of them.
				snapshot = _snapshots.Generate(pios_result);

				// Cache it for later.
				_cache.Set(domain, snapshot);

				// Return what we got.
				return snapshot;
			});
		}
	}
}
