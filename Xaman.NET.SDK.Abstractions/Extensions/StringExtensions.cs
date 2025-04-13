using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Xaman.NET.SDK.Extensions;

/// <summary>
/// Extension methods for string manipulation
/// </summary>
public static class StringExtensions
{
    private static readonly Regex RgxAccount = new(@"^r[1-9A-HJ-NP-Za-km-z]{25,33}$", RegexOptions.Compiled);
    private static readonly Regex RgxSHA512H = new("^[A-Fa-f0-9]{64}$", RegexOptions.Compiled);
    private static readonly Regex RgxUuid = new(@"^[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12}$", RegexOptions.Compiled);
    private static readonly Regex RgxCurrencyHex = new("^[a-fA-F0-9]{40}$", RegexOptions.Compiled);
    private static readonly Regex RgxDecodedHex = new("[a-zA-Z0-9]{3,}", RegexOptions.Compiled);
    private static readonly string HexReplacementPattern = "(00)+$";
    private static readonly string XRP = "XRP";
    private static readonly decimal XrpDrops = 1000000m;
    private static readonly decimal MaximumXrpValue = 100000000000m;
    
    /// <summary>
    /// Checks if a string is a valid XRPL account address
    /// </summary>
    /// <param name="input">The string to check</param>
    /// <returns>True if the string is a valid account address</returns>
    public static bool IsAccountAddress(this string? input)
    {
        return input != null && RgxAccount.IsMatch(input);
    }
    
    /// <summary>
    /// Checks if a string is a valid SHA-512Half hash
    /// </summary>
    /// <param name="input">The string to check</param>
    /// <returns>True if the string is a valid SHA-512Half hash</returns>
    public static bool IsSHA512H(this string? input)
    {
        return input != null && RgxSHA512H.IsMatch(input);
    }
    
    /// <summary>
    /// Checks if a string is a valid UUID
    /// </summary>
    /// <param name="input">The string to check</param>
    /// <returns>True if the string is a valid UUID</returns>
    public static bool IsValidUuid(this string? input)
    {
        return input != null && RgxUuid.IsMatch(input);
    }
    
    /// <summary>
    /// Formats a currency code to a standard representation
    /// </summary>
    /// <param name="currency">The currency code to format</param>
    /// <param name="maxLength">The maximum length of the formatted code</param>
    /// <returns>The formatted currency code</returns>
    public static string ToFormattedCurrency(this string currency, int maxLength = 12)
    {
        currency = currency.Trim();
        if (currency.Length == 3 && !currency.ToUpper().Equals(XRP))
        {
            return currency;
        }
        
        if (RgxCurrencyHex.IsMatch(currency))
        {
            var hex = Regex.Replace(currency, HexReplacementPattern, string.Empty);
            var bytes = Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
            
            if (hex.StartsWith("02"))
            {
                bytes = bytes.Skip(8).ToArray();
            }
            
            var decoded = Encoding.UTF8.GetString(bytes);
            if (decoded.Length > maxLength)
            {
                decoded = decoded[..maxLength];
            }
            
            if (RgxDecodedHex.IsMatch(decoded) && !currency.ToUpper().Equals(XRP))
            {
                return decoded;
            }
        }
        
        return "???";
    }
    
    /// <summary>
    /// Converts a string number to a decimal value
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The decimal value</returns>
    /// <exception cref="FormatException">Thrown when the string cannot be parsed</exception>
    public static decimal XrplStringNumberToDecimal(this string value)
    {
        if (!decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
        {
            throw new FormatException($"Unable to convert string number \"{value}\" to a decimal.");
        }
        
        return result;
    }
    
    /// <summary>
    /// Converts an XRP value to drops (1 XRP = 1,000,000 drops)
    /// </summary>
    /// <param name="value">The XRP value to convert</param>
    /// <returns>The value in drops as a string</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than the maximum allowed</exception>
    public static string XrpToDropsString(this decimal value)
    {
        if (value > MaximumXrpValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Maximum value of XRP is {MaximumXrpValue}");
        }
        
        return Math.Truncate(value * XrpDrops).ToString(CultureInfo.InvariantCulture);
    }
    
    /// <summary>
    /// Converts a drops value to an XRP decimal
    /// </summary>
    /// <param name="value">The drops value as a string</param>
    /// <returns>The XRP decimal value</returns>
    public static decimal XrpDropsToDecimal(this string value)
    {
        var decimalValue = value.XrplStringNumberToDecimal();
        return decimalValue / XrpDrops;
    }
    
    /// <summary>
    /// Computes the SHA-1 hash of a string
    /// </summary>
    /// <param name="value">The string to hash</param>
    /// <returns>The SHA-1 hash as a lowercase hexadecimal string</returns>
    public static string ToSha1Hash(this string value)
    {
        using var sha1Hash = SHA1.Create();
        var hashBytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        return string.Concat(hashBytes.Select(b => b.ToString("x2")));
    }
}