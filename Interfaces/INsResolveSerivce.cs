using System.Net;
using System.Threading.Tasks;

namespace KowWhoisApi.Interfaces
{
	public interface INsResolveSerivce
	{
		Task<IPAddress[]> Resolve(string domain);
	}
}
