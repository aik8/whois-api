using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface IDomainsService
	{
		Domain FindOrAdd(Domain domain);
	}
}
