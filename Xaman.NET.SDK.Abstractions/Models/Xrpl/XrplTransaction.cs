using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Xrpl;

/// <summary>
/// XRPL transaction details
/// </summary>
public class XrplTransaction
{
    /// <summary>
    /// The transaction type
    /// </summary>
    [JsonPropertyName("TransactionType")]
    public string TransactionType { get; set; } = string.Empty;
        
    /// <summary>
    /// The account that initiated the transaction
    /// </summary>
    [JsonPropertyName("Account")]
    public string Account { get; set; } = string.Empty;
        
    /// <summary>
    /// The fee paid for the transaction (in drops)
    /// </summary>
    [JsonPropertyName("Fee")]
    public string Fee { get; set; } = string.Empty;
        
    /// <summary>
    /// The destination account (for Payment type)
    /// </summary>
    [JsonPropertyName("Destination")]
    public string? Destination { get; set; }
        
    /// <summary>
    /// The amount (for Payment type)
    /// </summary>
    [JsonPropertyName("Amount")]
    public object? Amount { get; set; }
        
    /// <summary>
    /// The transaction hash
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;
        
    /// <summary>
    /// When the transaction was finalized
    /// </summary>
    [JsonPropertyName("date")]
    public ulong? Date { get; set; }
        
    /// <summary>
    /// The ledger index where this transaction was included
    /// </summary>
    [JsonPropertyName("ledger_index")]
    public uint? LedgerIndex { get; set; }
}