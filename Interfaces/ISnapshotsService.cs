using System.Collections.Generic;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface ISnapshotsService
	{
		Snapshot Create(IPiosResult piosResult);
		void Add(Snapshot snapshot);
		List<Snapshot> Get(uint? id, uint? domainId, string domainName);
		IPagedResponse<Snapshot> GetPaged(uint? domainId, string domainName, int page, int per_page);
	}
}
