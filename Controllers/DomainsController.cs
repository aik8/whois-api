using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrWhoisApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DomainsController : ControllerBase
	{
		private readonly IDomainsService _domains;

		public DomainsController(WhoisContext context, IDomainsService domains)
		{
			_domains = domains;
		}

		[HttpGet]
		[Route("{id?}")]
		public IActionResult Get(uint? id, [FromQuery] string name = null, [FromQuery] int page = 0, [FromQuery] int per_page = int.MaxValue)
		{
			if (id != null)
			{
				return Ok(_domains.Get((uint)id));
			}
			else
			{
				return Ok(_domains.Find(name, per_page, page));
			}
		}
	}
}
