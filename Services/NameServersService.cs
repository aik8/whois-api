using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;

namespace KowWhoisApi.Services
{
	public class NameServersService : INameServersService
	{
		private WhoisContext _context;

		public NameServersService(WhoisContext context)
		{
			_context = context;
		}

		public NameServer FindOrAdd(NameServer nameServer)
		{
			return _context.NameServers.SingleOrDefault(n => n.Name == nameServer.Name);
		}
	}
}
