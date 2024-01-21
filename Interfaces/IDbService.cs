namespace GrWhoisApi.Interfaces
{
	public interface IDbService<T>
	{
		/// <summary>
		/// Find or insert a specific entity.
		/// </summary>
		/// <param name="entity">The entity to be found.</param>
		/// <returns>The found or the freshly created entity.</returns>
		T FindOrInsert(T entity);

		/// <summary>
		/// Saves the given entity to the database.
		/// </summary>
		/// <param name="entity">The entity to be saved in the database.</param>
		/// <returns>The inserted entity.</returns>
		T Insert(T entity);

		/// <summary>
		/// Updates the given entity in the database.
		/// </summary>
		/// <param name="entity">The entity to be updated in the database.</param>
		/// <returns>The updated entity.</returns>
		/// <exception cref="System.ArgumentException">Thrown when the entity does not exist in the database.</exception>
		T Update(T entity);

		/// <summary>
		/// Get a specific entity.
		/// </summary>
		/// <param name="id">The entity ID to be found.</param>
		/// <returns>The requested entity, or null.</returns>
		T Get(uint id);

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
