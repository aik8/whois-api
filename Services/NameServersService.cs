using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Services
{
	public class NameServersService : INameServersService
	{
		private WhoisContext _context;

		public NameServersService(WhoisContext context)
		{
			_context = context;
		}

		public NameServer FindOrAdd(NameServer nameserver)
		{
			// Try to find the entity in the database.
			var from_db = _context.NameServers.SingleOrDefault(ns => ns.Name == nameserver.Name);

			// If it's not in the DB, create it. Either way, return the entity.
			if (from_db == null) return _context.NameServers.Add(nameserver).Entity;
			return from_db;
		}

		public NameServer Find(uint id)
		{
			return _context.NameServers.SingleOrDefault(ns => ns.Id == id);
		}

		public IPagedResponse<NameServer> Find(string name = null, int per_page = int.MaxValue, int page = 0)
		{
			var data = _context.NameServers
				.Where(ns => name == null || EF.Functions.Like(ns.Name, $"%{name}%"));

			var total = data.Count();

			var paged_data = data
				.Include(ns => ns.NameServerAddresses)
					.ThenInclude(nsa => nsa.Address)
				.OrderBy(ns => ns.Id)
				.Skip(per_page * page)
				.Take(per_page)
				.ToList();

			return new PagedResponse<NameServer>(paged_data, total, page, per_page);
		}
	}
}
