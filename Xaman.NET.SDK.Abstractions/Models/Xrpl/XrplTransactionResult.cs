using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Xrpl;

/// <summary>
/// XRPL transaction result model
/// </summary>
public class XrplTransactionResult
{
    /// <summary>
    /// Whether the transaction is validated on the ledger
    /// </summary>
    [JsonPropertyName("validated")]
    public bool Validated { get; set; }
        
    /// <summary>
    /// The status of the transaction
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
        
    /// <summary>
    /// The transaction metadata
    /// </summary>
    [JsonPropertyName("meta")]
    public XrplTransactionMeta? Meta { get; set; }
        
    /// <summary>
    /// The transaction itself
    /// </summary>
    [JsonPropertyName("tx")]
    public XrplTransaction Tx { get; set; } = new();
        
    /// <summary>
    /// The raw response from the XRPL
    /// </summary>
    public JsonDocument? RawResponse { get; set; }
}