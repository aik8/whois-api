using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Services
{
	public class SnapshotsService : ISnapshotsService
	{
		private WhoisContext _context;
		private INameServersService _nameServers;
		private IDomainsService _domains;
		private IRegistrarsService _registrars;
		private IAddressesService _addresses;

		public SnapshotsService(
			WhoisContext context,
			INameServersService nameServers,
			IDomainsService domains,
			IRegistrarsService registrars,
			IAddressesService addresses
			)
		{
			_context = context;
			_nameServers = nameServers;
			_domains = domains;
			_registrars = registrars;
			_addresses = addresses;
		}

		public Snapshot Generate(IPiosResult piosResult)
		{
			var snapshot = new Snapshot();

			snapshot.Domain = _domains.FindOrInsert(piosResult.Domain);
			var addresses = Dns.GetHostAddresses(snapshot.Domain.Name);
			foreach (var address in addresses)
			{
				var ip = _addresses.FindOrInsert(address);
				snapshot.Domain.Addresses.Add(ip);
			}

			if (piosResult.IsRegistered)
			{
				Registrar r = _registrars.FindOrInsert(piosResult.Registrar);

				foreach (var nameServer in piosResult.NameServers)
				{
					NameServer ns = _nameServers.FindOrInsert(nameServer);

					addresses = Dns.GetHostAddresses(ns.Name);
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
	}
}
