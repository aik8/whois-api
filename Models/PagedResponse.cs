using System;
using System.Collections.Generic;
using KowWhoisApi.Interfaces;

namespace KowWhoisApi.Models
{
	public class PagedResponse<T> : IPagedResponse<T>
	{
		public List<T> Data { get; private set; }
		public int Count { get; private set; }
		public int Page { get; private set; }
		public int PageCount { get; private set; }

		public PagedResponse(List<T> data, int count, int page, int per_page)
		{
			// Point to the data.
			Data = data;

			// Count the passed data.
			Count = count;

			// Keep the current page number.
			Page = page;

			// Calculate the page count.
			CalculatePages(per_page);
		}

		private void CalculatePages(int per_page)
		{
			double pages = Count / per_page;
			PageCount = (int)Math.Ceiling(pages);
		}
	}
}
