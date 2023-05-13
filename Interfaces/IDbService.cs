using System.Collections.Generic;

namespace KowWhoisApi.Interfaces
{
	public interface IDbService<T>
	{
		/// <summary>
		/// Find or add a specific entity.
		/// </summary>
		/// <param name="entity">The entity to be found.</param>
		/// <returns>The found or the freshly created entity.</returns>
		T FindOrAdd(T entity);

		/// <summary>
		/// Find a specific entity (or all entities, if no parameter is given)
		/// </summary>
		/// <param name="id">The entity ID to be found.</param>
		/// <returns>The requested entity, or null.</returns>
		T Find(uint id);

		/// <summary>
		/// Search for entities based on an identifying string property.
		/// </summary>
		/// <param name="property">The property to be used as a search criterion</param>
		/// <param name="per_page">The number of entities per page.</param>
		/// <param name="page">The page to fetch.</param>
		/// <returns>A nice, paged response containing the results.</returns>
		IPagedResponse<T> Find(string property = null, int per_page = int.MaxValue, int page = 0);
	}
}
