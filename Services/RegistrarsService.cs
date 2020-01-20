using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;

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
			return _context.Registrars.SingleOrDefault(r => r.Name == registrar.Name);
		}
	}
}
