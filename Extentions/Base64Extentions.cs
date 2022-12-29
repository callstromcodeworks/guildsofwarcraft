/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using System.Text;

namespace CCW.GoW.Extentions;

public static class Base64Extentions
{
    /// <summary>
    /// Encode string to Base64
    /// </summary>
    /// <param name="inputString">The string to encode.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string ToBase64(this string inputString)
    {
        byte[] inputStringAsByteArray = Encoding.UTF8.GetBytes(inputString);
        return Convert.ToBase64String(inputStringAsByteArray);
    }

    /// <summary>
    /// Decode string from Base64
    /// </summary>
    /// <param name="inputBase64">The Base64 string to decode.</param>
    /// <returns>The decoded string.</returns>
    public static string FromBase64(this string inputBase64)
    {
        byte[] inputBase64AsByteArray = Convert.FromBase64String(inputBase64);
        return Encoding.UTF8.GetString(inputBase64AsByteArray);
    }
}
