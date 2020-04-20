using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;

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
	}
}
