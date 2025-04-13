using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Xrpl;

/// <summary>
/// XRPL account data
/// </summary>
public class XrplAccountData
{
    /// <summary>
    /// The account address
    /// </summary>
    [JsonPropertyName("Account")]
    public string Account { get; set; } = string.Empty;
        
    /// <summary>
    /// The account balance (in drops)
    /// </summary>
    [JsonPropertyName("Balance")]
    public string Balance { get; set; } = string.Empty;
        
    /// <summary>
    /// The number of objects owned by this account
    /// </summary>
    [JsonPropertyName("OwnerCount")]
    public uint OwnerCount { get; set; }
        
    /// <summary>
    /// The account sequence number
    /// </summary>
    [JsonPropertyName("Sequence")]
    public uint Sequence { get; set; }
}