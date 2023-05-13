using System.Collections.Generic;

namespace KowWhoisApi.Interfaces
{
	public interface IPagedResponse<T>
	{
		List<T> Data { get; }
		int Count { get; }
		int Page { get; }
		int PageCount { get; }
	}
}
