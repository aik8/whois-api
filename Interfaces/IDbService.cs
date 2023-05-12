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
		/// Find a specific entity by ID.
		/// </summary>
		/// <param name="id">The entity ID to be found.</param>
		/// <returns>The requested entity, or null.</returns>
		T FindById(uint? id);

		/// <summary>
		/// Get all entities in the database.
		/// </summary>
		/// <returns>A List of entities in the database.</returns>
		List<T> Get();

		/// <summary>
		/// Get all entities in the database, in a paged manner.
		/// </summary>
		/// <param name="per_page">Entities per page</param>
		/// <param name="page">The page to get</param>
		/// <returns>A nice paged response.</returns>
		IPagedResponse<T> GetPaged(int per_page, int page);
	}
}
