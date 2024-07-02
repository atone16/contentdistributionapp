namespace CDA.Core
{
    public interface ICache
    {
        /// <summary>
        /// Checks whether the cache contains an entry with the given key.
        /// </summary>
        /// <param name="key">Key to check for.</param>
        /// <returns><c>true</c> if key exists; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// Deletes the item with the specified key.
        /// </summary>
        /// <param name="key">Key of item to delete.</param>
        /// <returns><c>true</c> if item was found and deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(string key);

        /// <summary>
        /// Gets the item with the given key, or the default value for <typeparamref name="T"/> if the item doesn't exist.
        /// </summary>
        /// <typeparam name="T">Type of item to get.</typeparam>
        /// <param name="key">Key of item to look up</param>
        /// <returns>Item having given key or default value for <typeparamref name="T"/> if not found.</returns>
        Task<T> GetAsync<T>(string key) where T : class;

        /// <summary>
        /// Adds the given item to the cache using the given key.
        /// </summary>
        /// <param name="key">Key to give the item in the cache.</param>
        /// <param name="item">Item to store in the cache.</param>
        Task SetAsync(string key, object item);

        Task SetAddAsync(string key, object item);

        Task SetRemoveAsync(string key, string itemId);

        Task<List<T>> GetItemByKey<T>(string key) where T : class;
    }

}
