using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Xrpl;

/// <summary>
/// XRPL transaction metadata
/// </summary>
public class XrplTransactionMeta
{
    /// <summary>
    /// The transaction result
    /// </summary>
    [JsonPropertyName("TransactionResult")]
    public string TransactionResult { get; set; } = string.Empty;
        
    /// <summary>
    /// The delivered amount (important for partial payments)
    /// </summary>
    [JsonPropertyName("delivered_amount")]
    public object? DeliveredAmount { get; set; }
        
    /// <summary>
    /// The balance changes caused by the transaction
    /// </summary>
    [JsonPropertyName("AffectedNodes")]
    public JsonElement? AffectedNodes { get; set; }
}