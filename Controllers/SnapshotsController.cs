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
	public class SnapshotsController : ControllerBase
	{
		private readonly WhoisContext _context;
		private readonly ILogger _logger;

		public SnapshotsController(WhoisContext context, ILogger<SnapshotsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public List<Snapshot> GetAll([FromQuery] uint domainId = 0, [FromQuery] string domainName = null, [FromQuery] int per_page = 0, [FromQuery] int page = 0)
		{
			// Get the value of domainId.
			uint id = domainId;

			// If there is a domainName, ignore the domainId.
			if (domainName != null)
			{
				id = _context.Domains.SingleOrDefault(d => d.Name == domainName)?.Id ?? 0;
			}

			// Run the main part of the query.
			var query = _context.Snapshots
					.Where(s => id == 0 ? true : s.DomainId == id)
					.OrderBy(s => s.CreatedAt)
					.Include(s => s.Domain)
					.Include(s => s.Registrar)
					.Include(s => s.SnapshotNameServers)
					.ThenInclude(sns => sns.NameServer);

			// Add pagination, if it is requested.
			if (per_page > 0)
			{
				return query.Skip(per_page * page).Take(per_page).ToList();
			}
			else
			{
				return query.ToList();
			}
		}
	}
}
