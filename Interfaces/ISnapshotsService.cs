using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface ISnapshotsService
	{
		Snapshot Create(IPiosResult piosResult);
		void Save(Snapshot snapshot);
	}
}
