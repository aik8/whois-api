using System.Threading.Tasks;

namespace KowWhoisApi.Interfaces
{
	public interface IPiosService
	{
		Task<IPiosResult> AskPios(string domain, bool fresh = false);
	}
}
