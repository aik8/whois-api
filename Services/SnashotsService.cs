using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;

namespace KowWhoisApi.Services
{
	public class SnapshotsService : ISnapshotsService
	{
		private WhoisContext _context;
		private INameServersService _nameServers;
		private IDomainsService _domains;
		private IRegistrarsService _registrars;

		public SnapshotsService(WhoisContext context, INameServersService nameServers, IDomainsService domains, IRegistrarsService registrars)
		{
			_context = context;
			_nameServers = nameServers;
			_domains = domains;
			_registrars = registrars;
		}

		public Snapshot Create(IPiosResult piosResult)
		{
			var snapshot = new Snapshot();

			Domain d = _domains.FindOrAdd(piosResult.Domain);
			snapshot.Domain = d != null ? d : piosResult.Domain;

			if (piosResult.IsRegistered)
			{
				Registrar r = _registrars.FindOrAdd(piosResult.Registrar);
				snapshot.Registrar = r != null ? r : piosResult.Registrar;

				foreach (var ns in piosResult.NameServers)
				{
					NameServer n = _nameServers.FindOrAdd(ns);

					if (n != null)
					{
						snapshot.SnapshotNameServers.Add(new SnapshotNameServer { NameServer = n });
					}
					else
					{
						snapshot.SnapshotNameServers.Add(new SnapshotNameServer { NameServer = ns });
					}
				}
			}

			return snapshot;
		}

		public void Save(Snapshot snapshot)
		{
			_context.Snapshots.Add(snapshot);
			_context.SaveChanges();
		}
	}
}
