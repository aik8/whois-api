using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface IDomainsService
	{
		Domain Get(Domain domain);
		List<Domain> Find(uint? id, string name);
		IPagedResponse<Domain> FindPaged(string name, int per_page, int page);
	}
}
