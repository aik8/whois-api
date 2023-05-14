using System.Collections.Generic;
using System.Net;
using KowWhoisApi.Models;

namespace KowWhoisApi.Interfaces
{
	public interface IAddressesService : IDbService<Address>
	{
		/// <summary>
		/// Find an <see cref="Address"/> by providing an <see cref="IPAddress"/> instance.
		/// </summary>
		/// <param name="address">The <see cref="IPAddress"/> to be used as criterion</param>
		/// <returns>An <see cref="Address"/> object.</returns>
		public Address FindOrInsert(IPAddress address);
	}
}
