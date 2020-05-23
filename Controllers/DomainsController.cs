using System.Collections.Generic;
using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KowWhoisApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DomainsController : ControllerBase
	{
		private readonly WhoisContext _context;

		public DomainsController(WhoisContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("{id?}")]
		public List<Domain> Get(uint? id, [FromQuery] string name = null, [FromQuery] int page = 0, [FromQuery] int per_page = int.MaxValue)
		{
			return _context.Domains
				.Where(d => id == null || d.Id == id)
				.Where(d => name == null || d.Name == name)
				.Include(d => d.Snapshots)
				.Skip(page * per_page)
				.Take(per_page)
				.ToList();
		}
	}
}
