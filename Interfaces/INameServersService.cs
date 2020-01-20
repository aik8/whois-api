using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface INameServersService
	{
		NameServer FindOrAdd(NameServer nameServer);
	}
}
