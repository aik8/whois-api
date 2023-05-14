using System.Collections.Generic;
using System.Threading.Tasks;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface ISnapshotsService : IDbService<Snapshot>
	{
		/// <summary>
		/// Creates a complete <see cref="Snapshot"/> using the given <see cref="IPiosResult"/>.
		/// </summary>
		/// <param name="piosResult">An <see cref="IPiosResult"/> that will become the basis for the <see cref="Snapshot"/></param>
		/// <returns>A <see cref="Task"/> that resolves to the newly created <see cref="Snapshot"/>.</returns>
		Snapshot Find(IPiosResult piosResult);

		void Insert(Snapshot snapshot);
		Snapshot Generate(IPiosResult piosResult);
	}
}
