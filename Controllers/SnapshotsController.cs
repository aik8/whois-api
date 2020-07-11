using System.Collections.Generic;
using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
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
		private readonly ISnapshotsService _snapshots;
		private readonly ILogger _logger;

		public SnapshotsController(ISnapshotsService snapshots, ILogger<SnapshotsController> logger)
		{
			_snapshots = snapshots;
			_logger = logger;
		}

		[HttpGet]
		[Route("{id?}")]
		public IActionResult Get(uint? id, [FromQuery] uint? domainId, [FromQuery] string domainName = null, [FromQuery] int per_page = int.MaxValue, [FromQuery] int page = 0)
		{
			if (per_page == int.MaxValue)
			{
				return Ok(_snapshots.Get(id, domainId, domainName));
			}
			else
			{
				return Ok(_snapshots.GetPaged(domainId, domainName, page, per_page));
			}
		}
	}
}
