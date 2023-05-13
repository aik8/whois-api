using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Services
{
	public class RegistrarsService : IRegistrarsService
	{
		private WhoisContext _context;

		public RegistrarsService(WhoisContext context)
		{
			_context = context;
		}

		public Registrar FindOrAdd(Registrar registrar)
		{
			// Try to find the entity in the database.
			var from_db = _context.Registrars.SingleOrDefault(d => d.Name == registrar.Name);

			// If it's not in the DB, create it. Either way, return the entity.
			if (from_db == null) return _context.Registrars.Add(registrar).Entity;
			return from_db;
		}

		public Registrar Find(uint id)
		{
			return _context.Registrars.SingleOrDefault(r => r.Id == id);
		}

		public IPagedResponse<Registrar> Find(string name = null, int per_page = int.MaxValue, int page = 0)
		{
			var data = _context.Registrars
				.Where(r => name == null || EF.Functions.Like(r.Name, $"%{name}%"));

			var total = data.Count();

			var paged_data = data
				.OrderBy(r => r.Name)
				.Skip(per_page * page)
				.Take(per_page)
				.ToList();

			return new PagedResponse<Registrar>(paged_data, total, page, per_page);
		}
	}
}
