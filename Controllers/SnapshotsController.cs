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
	public class SnapshotsController : ControllerBase
	{
		private readonly WhoisContext _context;

		public SnapshotsController(WhoisContext context)
		{
			_context = context;
		}

		[HttpGet]
		public List<Snapshot> GetAll()
		{
			return _context.Snapshots
				.Include(s => s.Domain)
				.Include(s => s.Registrar)
				.Include(s => s.SnapshotNameServers)
				.ThenInclude(sns => sns.NameServer)
				.ToList();
		}
	}
}
