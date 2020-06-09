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
		[Route("{id?}")]
		public List<Snapshot> Get(uint? id, [FromQuery] uint? domainId, [FromQuery] string domainName = null, [FromQuery] int per_page = int.MaxValue, [FromQuery] int page = 0)
		{
			return _context.Snapshots
				.Where(s => id == null || s.Id == id)
				.Where(s => domainId == null || s.DomainId == domainId)
				.Where(s => domainName == null || s.Domain.Name == domainName)
				.OrderBy(s => s.CreatedAt)
				.Skip(page * per_page)
				.Take(per_page)
				.Include(s => s.Domain)
				.Include(s => s.Registrar)
				.Include(s => s.SnapshotNameServers)
				.ThenInclude(sns => sns.NameServer)
				.ToList();
		}
	}
}
