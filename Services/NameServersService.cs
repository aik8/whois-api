using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;

namespace KowWhoisApi.Services
{
	public class NameServersService : INameServersService
	{
		private WhoisContext _context;
		private readonly INsResolveSerivce _resolver;

		public NameServersService(WhoisContext context, INsResolveSerivce resolver)
		{
			_context = context;
			_resolver = resolver;
		}

		public NameServer Find(NameServer nameServer)
		{
			return _context.NameServers.SingleOrDefault(ns => ns.Name == nameServer.Name);
		}
	}
}
