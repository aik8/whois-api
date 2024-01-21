namespace GrWhoisApi.Interfaces
{
	public interface ICacheService<T>
	{
		/// <summary>
		/// Set a key and value into the cache.
		/// </summary>
		/// <param name="key">The key for the value to be set</param>
		/// <param name="value">The value to be set into the cache</param>
		void Set(string key, T value);

		/// <summary>
		/// Try to recall the value for the given key from the cache.
		/// </summary>
		/// <param name="key">The key for the value to be fetched</param>
		/// <returns>The value assigned to the given key or null, if nothing is found.</returns>
		T Get(string key);
	}
}
