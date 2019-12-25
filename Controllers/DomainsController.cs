using System.Collections.Generic;
using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public List<Domain> GetAll()
		{
			return _context.Domains
				.Include(d => d.Snapshots)
				.ToList();
		}
	}
}
