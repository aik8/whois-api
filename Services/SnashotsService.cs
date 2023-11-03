using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KowWhoisApi.Services
{
	public class SnapshotsService : ISnapshotsService
	{
		private readonly ILogger _logger;
		private readonly WhoisContext _context;
		private readonly INameServersService _nameServers;
		private readonly IDomainsService _domains;
		private readonly IRegistrarsService _registrars;
		private readonly IAddressesService _addresses;

		public SnapshotsService(
			ILogger<SnapshotsService> logger,
			WhoisContext context,
			INameServersService nameServers,
			IDomainsService domains,
			IRegistrarsService registrars,
			IAddressesService addresses
			)
		{
			_logger = logger;
			_context = context;
			_nameServers = nameServers;
			_domains = domains;
			_registrars = registrars;
			_addresses = addresses;
		}

		public Snapshot Generate(IPiosResult piosResult)
		{
			// Create an empty snapshot.
			var snapshot = new Snapshot();

			// Is the domain registered?
			snapshot.IsRegistered = piosResult.IsRegistered;

			// Add the basic domain information.
			snapshot.Domain = _domains.FindOrInsert(piosResult.Domain);

			// Add addresses to the domain.
			var addresses = Resolve(snapshot.Domain.Name);
			foreach (var address in addresses)
			{
				var ip = _addresses.FindOrInsert(address);
				snapshot.Domain.Addresses.Add(ip);
			}

			if (piosResult.IsRegistered)
			{
				snapshot.Registrar = _registrars.FindOrInsert(piosResult.Registrar);

				foreach (var nameServer in piosResult.NameServers)
				{
					NameServer ns = _nameServers.FindOrInsert(nameServer);

					addresses = Resolve(ns.Name);
					foreach (var address in addresses)
					{
						var ip = _addresses.FindOrInsert(address);
						ns.Addresses.Add(ip);
					}

					snapshot.NameServers.Add(ns);
				}
			}

			Insert(snapshot);
			return snapshot;
		}

		public void Insert(Snapshot snapshot)
		{
			_context.Snapshots.Add(snapshot);
			_context.SaveChanges();
		}

		public Snapshot Find(uint id)
		{
			return _context.Snapshots
				.Include(s => s.Domain)
				.Include(s => s.Registrar)
				.Include(s => s.NameServers)
					.ThenInclude(ns => ns.NameServerAddresses)
						.ThenInclude(nsa => nsa.Address)
				.SingleOrDefault(s => s.Id == id);
		}

		public IPagedResponse<Snapshot> Find(uint domainId, int per_page, int page)
		{
			var data = _context.Snapshots
				.Where(s => s.DomainId == domainId)
				.OrderByDescending(s => s.CreatedAt);

			var total = data.Count();

			var paged_data = data
				.Include(s => s.Domain)
				.Include(s => s.Registrar)
				.Include(s => s.NameServers)
					.ThenInclude(ns => ns.NameServerAddresses)
						.ThenInclude(nsa => nsa.Address)
				.ToList();

			return new PagedResponse<Snapshot>(paged_data, total, page, per_page);
		}

		public IPagedResponse<Snapshot> Find(string domainName, int per_page, int page)
		{
			var data = _context.Snapshots
				.Where(s => s.Domain.Name == domainName)
				.OrderByDescending(s => s.CreatedAt);

			var total = data.Count();

			var paged_data = data
				.Include(s => s.Domain)
				.Include(s => s.Registrar)
				.Include(s => s.NameServers)
					.ThenInclude(ns => ns.NameServerAddresses)
						.ThenInclude(nsa => nsa.Address)
				.ToList();

			return new PagedResponse<Snapshot>(paged_data, total, page, per_page);
		}

		public Snapshot Find(IPiosResult piosResult)
		{
			throw new System.NotImplementedException();
		}

		public Snapshot FindOrInsert(Snapshot entity)
		{
			throw new System.NotImplementedException();
		}

		private IPAddress[] Resolve(string hostName)
		{
			try
			{
				return Dns.GetHostAddresses(hostName);
			}
			catch
			{
				_logger.LogWarning($"Host {hostName} not found.");
				return new IPAddress[] { };
			}
		}
	}
}
