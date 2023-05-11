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

		public Address Get(Address address)
		{
			return _context.Addresses.SingleOrDefault(a => a.Ip == address.Ip);
		}

		public List<Address> Find(string address)
		{
			return _context.Addresses
				.Where(a => address == null || EF.Functions.Like(a.Ip, $"%{address}%"))
				.OrderBy(a => a.Ip)
				.ToList();
		}
	}
}
