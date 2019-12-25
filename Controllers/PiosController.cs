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
		private WhoisContext _context;
		private IPiosService _pios;

		public PiosController(WhoisContext context, IPiosService pios)
		{
			_context = context;
			_pios = pios;
		}

		[HttpGet]
		public ActionResult<IPiosResult> Get([FromQuery] string domain, [FromQuery] bool fast = false)
		{
			// If this request comes from an automated system,
			// there is no need for it to wait for a response.
			if (fast)
			{
				// Start the whole data collection chain...
				Task.Factory.StartNew(() =>
				{
					var result = _pios.AskPios(domain);
					Console.WriteLine(result.ToString());
				});

				// ... and send immediately just a 201 to the requester.
				return StatusCode(201);
			}

			var piosResult = _pios.AskPios(domain);
			var snapshot = CreateSnapshot(piosResult);

			Domain d = FindOrAddDomain(snapshot.Domain);
			if (d != null) snapshot.Domain = d;

			if (piosResult.IsRegistered)
			{
				Registrar r = FindOrAddRegistrar(snapshot.Registrar);
				if (r != null) snapshot.Registrar = r;
			}

			SaveSnapshot(snapshot);

			// Otherwise do return something.
			return new ActionResult<IPiosResult>(piosResult);
		}

		private Snapshot CreateSnapshot(IPiosResult piosResult)
		{
			var snapshot = new Snapshot();
			snapshot.Domain = piosResult.Domain;
			snapshot.Registrar = piosResult.Registrar;

			foreach (var ns in piosResult.NameServers)
			{
				NameServer n = FindOrAddNameServer(ns);

				if (n != null) {
					snapshot.SnapshotNameServers.Add(new SnapshotNameServer { NameServer = n });
				} else {
					snapshot.SnapshotNameServers.Add(new SnapshotNameServer { NameServer = ns });
				}
			}

			return snapshot;
		}

		private void SaveSnapshot(Snapshot snapshot)
		{
			_context.Snapshots.Add(snapshot);
			_context.SaveChanges();
		}

		private Domain FindOrAddDomain(Domain domain)
		{
			return _context.Domains.SingleOrDefault(d => d.Name == domain.Name);
		}

		private Registrar FindOrAddRegistrar(Registrar registrar)
		{
			return _context.Registrars.SingleOrDefault(r => r.Name == registrar.Name);
		}

		private NameServer FindOrAddNameServer(NameServer nameServer)
		{
			return _context.NameServers.SingleOrDefault(n => n.Name == nameServer.Name);
		}
	}
}
