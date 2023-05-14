using System.Collections.Generic;
using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Services
{
	public class DomainsService : IDomainsService
	{
		private WhoisContext _context;

		public DomainsService(WhoisContext context)
		{
			_context = context;
		}

		public Domain FindOrInsert(Domain domain)
		{
			// Try to find the entity in the database.
			var from_db = _context.Domains.SingleOrDefault(d => d.Name == domain.Name);

			// If it's not in the DB, create it. Either way, return the entity.
			if (from_db == null) return _context.Domains.Add(domain).Entity;
			return from_db;
		}

		public Domain Find(uint id)
		{
			return _context.Domains.SingleOrDefault(d => d.Id == id);
		}

		public IPagedResponse<Domain> Find(string name = null, int per_page = int.MaxValue, int page = 0)
		{
			var data = _context.Domains
				.Where(dom => name == null || EF.Functions.Like(dom.Name, $"%{name}%"));

			var total = data.Count();

			var paged_data = data
				.Include(d => d.DomainAddresses)
					.ThenInclude(da => da.Address)
				.OrderBy(dom => dom.Name)
				.Skip(page * per_page)
				.Take(per_page)
				.ToList();

			return new PagedResponse<Domain>(paged_data, total, page, per_page);
		}
	}
}
