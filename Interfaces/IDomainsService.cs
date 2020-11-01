using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface IDomainsService
	{
		Domain FindOrAdd(Domain domain);
		List<Domain> Get(uint? id, string name);
		IPagedResponse<Domain> GetPaged(string name, int per_page, int page);
	}
}
