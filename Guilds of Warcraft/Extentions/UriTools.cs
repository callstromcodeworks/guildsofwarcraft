/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using System.Text;

namespace CCW.GoW.Extentions;

public static class UriTools
{
    /// <summary>
    /// Add query parameter to Uri
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <param name="query">The full query (key=value) to add</param>
    /// <returns>The Uri object to chain calls.</returns>
    public static UriBuilder AddQuery(this UriBuilder uri, string query)
    {
        if (uri.Query == string.Empty)
        {
            uri.Query = query;
        }
        else
        {
            uri.Query += $"&{query}";
        }
        return uri;
    }
    /// <summary>
    /// Add query parameter to Uri
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <param name="key">The key component of the query</param>
    /// <param name="value">The value component of the query</param>
    /// <returns>The Uri object to chain calls.</returns>
    public static UriBuilder AddQuery(this UriBuilder uri, string key, string value)
    {
        if (uri.Query == string.Empty) uri.Query = $"{key}={value}";
        else uri.Query += $"&{key}={value}";
        return uri;
    }
}
