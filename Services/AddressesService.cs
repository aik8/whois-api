using System.Linq;
using System.Net;
using GrWhoisApi.Data;
using GrWhoisApi.Interfaces;
using GrWhoisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GrWhoisApi.Services
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
			if (from_db == null)
			{
				var fresh = _context.Addresses.Add(address).Entity;
				_context.SaveChanges();
				return fresh;
			}

			return from_db;
		}

		public Address FindOrInsert(IPAddress address)
		{
			// Try to find the address in the database.
			var from_db = _context.Addresses.SingleOrDefault(a => a.IpRaw == address.GetAddressBytes());

			// If it's not in the DB, create it. Either way, return the Address.
			if (from_db == null) return _context.Addresses.Add(new Address(address)).Entity;
			return from_db;
		}

		public Address Get(uint id)
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

		public Address Insert(Address entity)
		{
			throw new System.NotImplementedException();
		}

		public Address Update(Address entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
