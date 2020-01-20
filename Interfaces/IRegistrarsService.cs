using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface IRegistrarsService
	{
		Registrar FindOrAdd(Registrar registrar);
	}
}
