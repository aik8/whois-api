using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KowWhoisApi.Models;
using KowWhoisApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PiosController : ControllerBase
	{
		private IPiosService _pios;

		public PiosController(IPiosService pios)
		{
			_pios = pios;
		}

		[HttpGet]
		public ActionResult<IPiosResult> Get([FromQuery] string domain, [FromQuery] bool fast = false)
		{
			// If this request comes from an automated system,
			// there is no need for it to wait for a response.
			if (fast) {
				// Start the whole data collection chain...
				Task.Factory.StartNew(() => {
					var result = _pios.AskPios(domain);
					Console.WriteLine(result.ToString());
				});

				// ... and send immediately just a 201 to the requester.
				return StatusCode(201);
			}

			// Otherwise do return something.
			return new ActionResult<IPiosResult>(_pios.AskPios(domain));
		}
	}
}
