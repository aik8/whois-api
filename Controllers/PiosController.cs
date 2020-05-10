using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KowWhoisApi.Models;
using KowWhoisApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using KowWhoisApi.Data;

namespace KowWhoisApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PiosController : ControllerBase
	{
		private IPiosService _pios;
		private ISnapshotsService _snapshots;

		public PiosController(IPiosService pios, ISnapshotsService snapshots)
		{
			_pios = pios;
			_snapshots = snapshots;
		}

		[HttpGet]
		public ActionResult<IPiosResult> Get([FromQuery] string domain, [FromQuery] bool fast = false)
		{
			// Check if we got a valid query.
			if (domain == null) {
				return BadRequest();
			}

			// If this request comes from an automated system,
			// there is no need for it to wait for a response.
			if (fast)
			{
				// Start the whole data collection chain...
				Response.OnCompleted(() => Ask(domain));

				// ... and send immediately just a 200 to the requester.
				return Accepted();
			}

			// Wait for the result.
			var task = Ask(domain);
			task.Wait();

			var piosResult = task.Result;

			// Otherwise do return something.
			return CreatedAtAction(nameof(Get), piosResult);
		}

		private Task<IPiosResult> Ask(string domain)
		{
			return Task.Factory.StartNew(() =>
			{
				var result = _pios.AskPios(domain);
				var snapshot = _snapshots.Create(result);
				_snapshots.Save(snapshot);

				return result;
			});
		}
	}
}
