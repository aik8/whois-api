using System.Collections.Generic;
using System.Linq;
using KowWhoisApi.Data;
using KowWhoisApi.Interfaces;
using KowWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KowWhoisApi.Services
{
	public class AddressesService : IAddressesService
	{
		private WhoisContext _context;

		public AddressesService(WhoisContext context)
		{
			_context = context;
		}

		public Address FindOrInsert(Address address)
		{
			// Try to find the address in the database.
			var from_db = _context.Addresses.SingleOrDefault(a => a.IpRaw == address.IpRaw);

			// If it's not in the DB, create it. Either way, return the Address.
			if (from_db == null) return _context.Addresses.Add(address).Entity;
			return from_db;
		}

		public Address Find(uint id)
		{
			return _context.Addresses.SingleOrDefault(a => a.Id == id);
		}

		public IPagedResponse<Address> Find(string address = null, int per_page = int.MaxValue, int page = 0)
		{
			var data = _context.Addresses
				.Where(a => address == null || EF.Functions.Like(a.Ip, $"%{address}%"));

			var total = data.Count();

			var paged_data = data
				.OrderBy(a => a.Ip)
				.Skip(page * per_page)
				.Take(per_page)
				.ToList();

			return new PagedResponse<Address>(paged_data, total, page, per_page);
		}
	}
}
