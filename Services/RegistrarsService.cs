using System.Linq;
using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using GrWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GrWhoisApi.Services
{
	public class RegistrarsService : IRegistrarsService
	{
		private WhoisContext _context;

		public RegistrarsService(WhoisContext context)
		{
			_context = context;
		}

		public Registrar FindOrInsert(Registrar registrar)
		{
			// Try to find the entity in the database.
			var from_db = _context.Registrars.SingleOrDefault(r => r.Name == registrar.Name);

			// If it's not in the DB, create it. Either way, return the entity.
			if (from_db == null)
			{
				var fresh = _context.Registrars.Add(registrar).Entity;
				_context.SaveChanges();
				return fresh;
			}

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
