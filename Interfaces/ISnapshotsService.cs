using System.Collections.Generic;
using System.Threading.Tasks;
using GrWhoisApi.Models;

namespace GrWhoisApi.Interfaces
{
	public interface ISnapshotsService : IDbService<Snapshot>
	{
		/// <summary>
		/// Creates a complete <see cref="Snapshot"/> using the given <see cref="IPiosResult"/>.
		/// </summary>
		/// <param name="piosResult">An <see cref="IPiosResult"/> that will become the basis for the <see cref="Snapshot"/></param>
		/// <returns>A <see cref="Task"/> that resolves to the newly created <see cref="Snapshot"/>.</returns>
		Snapshot Find(IPiosResult piosResult);

		Snapshot Generate(IPiosResult piosResult);
	}
}
