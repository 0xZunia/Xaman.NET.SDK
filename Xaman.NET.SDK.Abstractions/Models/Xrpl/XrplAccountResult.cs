using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Xrpl;

/// <summary>
/// XRPL account information result
/// </summary>
public class XrplAccountResult
{
    /// <summary>
    /// The account data
    /// </summary>
    [JsonPropertyName("account_data")]
    public XrplAccountData AccountData { get; set; } = new();
        
    /// <summary>
    /// Whether the result is validated
    /// </summary>
    [JsonPropertyName("validated")]
    public bool Validated { get; set; }
}