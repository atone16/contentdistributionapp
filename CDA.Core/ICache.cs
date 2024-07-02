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
        /// Gets the item with the given key, or the default value for <typeparamref name="T"/> if the item doesn't exist.
        /// </summary>
        /// <typeparam name="T">Type of item to get.</typeparam>
        /// <param name="key">Key of item to look up</param>
        /// <returns>Item having given key or default value for <typeparamref name="T"/> if not found.</returns>
        Task<T> GetAsync<T>(string key)
            where T : class;

        /// <summary>
        /// Adds the given item to the cache using the given key.
        /// </summary>
        /// <param name="key">Key to give the item in the cache.</param>
        /// <param name="item">Item to store in the cache.</param>
        Task SetAsync(string key, object item);

        /// <summary>
        /// Deletes the item with the specified key.
        /// </summary>
        /// <param name="key">Key of item to delete.</param>
        /// <returns><c>true</c> if item was found and deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(string key);

        /// <summary>
        /// Gets the value of the given field in the specified hash
        /// as an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item to get.</typeparam>
        /// <param name="hashKey">The key of the hash.</param>
        /// <param name="hashField">The field in the hash whose value to get.</param>
        /// <returns>Item at specified hash field or the default value for <typeparamref name="T"/> if not found.</returns>
        Task<T> HashGetAsync<T>(string hashKey, string hashField)
            where T : class;

        /// <summary>
        /// Gets the values of the given fields from the specified hash.
        /// </summary>
        /// <typeparam name="T">The type of the items to get</typeparam>
        /// <param name="hashKey">The key of the hash.</param>
        /// <param name="hashFields">The hash fields whose values to get.</param>
        /// <returns>Dictionary whose key is one of the field names given by <paramref name="hashFields"/>
        /// and whose value is the value of that field as type <typeparamref name="T"/> or the default value
        /// for <typeparamref name="T"/> if the field was not found.</returns>
        Task<Dictionary<string, T>> HashGetAsync<T>(string hashKey, IEnumerable<string> hashFields)
            where T : class;

        /// <summary>
        /// Gets the values of all fields of the given hash.
        /// </summary>
        /// <typeparam name="T">The type of the field values.</typeparam>
        /// <param name="hashKey">The key of the hash.</param>
        /// <returns>All field values as <typeparamref name="T"/> or an empty list if the hash was not found.</returns>
        Task<List<T>> HashGetAllAsync<T>(string hashKey)
            where T : class;

        /// <summary>
        /// Gets a list of all field names in the given hash.
        /// </summary>
        /// <param name="hashKey">Key of hash whose fields to get.</param>
        /// <returns>Names of all fields in hash, or an empty list if hash doesn't exist.</returns>
        Task<List<string>> HashFieldsAsync(string hashKey);

        /// <summary>
        /// Sets the given field of the specified hash to the given value.
        /// If the hash does not exist, it is created.
        /// </summary>
        /// <param name="hashKey">The key for the hash.</param>
        /// <param name="hashField">The field in the hash to set.</param>
        /// <param name="item">The item to set at the hash field given by <paramref name="hashField"/>.</param>
        Task HashSetAsync(string hashKey, string hashField, object item);

        /// <summary>
        /// Sets the given fields in the specified hash to the given values.
        /// </summary>
        /// <param name="hashKey">The key for the hash.</param>
        /// <param name="values">The field name-value pairs to set in the hash.</param>
        Task HashSetAsync(string hashKey, IEnumerable<KeyValuePair<string, object>> values);

        /// <summary>
        /// Deletes the given field from the given hash.
        /// </summary>
        /// <param name="hashKey">Key of hash to delete field from.</param>
        /// <param name="hashField">Name of field to delete.</param>
        /// <returns>Whether item was deleted.</returns>
        Task<bool> HashDeleteAsync(string hashKey, string hashField);

        /// <summary>
        /// Deletes the given fields from the given hash.
        /// </summary>
        /// <param name="hashKey">Key of hash to delete fields from.</param>
        /// <param name="hashFields">Names of fields to delete.</param>
        /// <returns>Number of fields that were deleted.</returns>
        Task<long> HashDeleteAsync(string hashKey, IEnumerable<string> hashFields);

        /// <summary>
        /// Returns a value indicating whether the given hash contains the given field.
        /// </summary>
        /// <param name="hashKey">Key of the hash.</param>
        /// <param name="hashField">Name of field whose existence to check.</param>
        /// <returns>Whether field exists.</returns>
        Task<bool> HashExistsAsync(string hashKey, string hashField);

        /// <summary>
        /// Removes all data from the cache.
        /// </summary>
        Task DeleteAllAsync();
    }

}
