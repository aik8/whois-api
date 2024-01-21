using System.Linq;
using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using GrWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GrWhoisApi.Services
{
	public class NameServersService : INameServersService
	{
		private WhoisContext _context;

		public NameServersService(WhoisContext context)
		{
			_context = context;
		}

		public NameServer FindOrInsert(NameServer nameserver)
		{
			// Try to find the entity in the database.
			var from_db = _context.NameServers.SingleOrDefault(ns => ns.Name == nameserver.Name);

			// If it's not in the DB, create it. Either way, return the entity.
			if (from_db == null)
			{
				var fresh = _context.NameServers.Add(nameserver).Entity;
				_context.SaveChanges();
				return fresh;
			}

			return from_db;
		}

		public NameServer Get(uint id)
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

		public NameServer Insert(NameServer entity)
		{
			throw new System.NotImplementedException();
		}

		public NameServer Update(NameServer entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
