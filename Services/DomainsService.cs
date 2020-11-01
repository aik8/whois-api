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

		public Domain FindOrAdd(Domain domain)
		{
			// Check that proper input has been passed.
			if (domain == null) return null;

			// Return the query result.
			return _context.Domains.SingleOrDefault(d => d.Name == domain.Name);
		}

		public List<Domain> Get(uint? id, string name = null)
		{
			return _context.Domains
				.Where(dom => id == null || dom.Id == id)
				.Where(dom => name == null || EF.Functions.Like(dom.Name, $"%{name}%"))
				.OrderBy(dom => dom.Name)
				.ToList();
		}

		public IPagedResponse<Domain> GetPaged(string name = null, int per_page = int.MaxValue, int page = 0)
		{
			var data = _context.Domains
				.Where(dom => name == null || EF.Functions.Like(dom.Name, $"%name%"))
				.OrderBy(dom => dom.Name)
				.Skip(page * per_page)
				.Take(per_page)
				.ToList();

			var total = _context.Domains.Count();

			return new PagedResponse<Domain>(data, total, page, per_page);
		}
	}
}
