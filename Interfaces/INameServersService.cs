using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface INameServersService
	{
		NameServer Find(NameServer nameServer);
	}
}
