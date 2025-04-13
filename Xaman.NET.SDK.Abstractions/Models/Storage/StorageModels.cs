using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Storage;

/// <summary>
/// Response from fetching app storage
/// </summary>
public class StorageResponse
{
    /// <summary>
    /// Application details
    /// </summary>
    [JsonPropertyName("application")]
    public StorageApplication Application { get; set; } = default!;
    
    /// <summary>
    /// The stored data
    /// </summary>
    [JsonPropertyName("data")]
    public JsonDocument? Data { get; set; }
}

/// <summary>
/// Application information for storage operations
/// </summary>
public class StorageApplication
{
    /// <summary>
    /// The name of the application
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The UUID of the application
    /// </summary>
    [JsonPropertyName("uuidv4")]
    public string Uuidv4 { get; set; } = default!;
}

/// <summary>
/// Response from storing or clearing app storage
/// </summary>
public class StorageStoreResponse : StorageResponse
{
    /// <summary>
    /// Indicates if the data was successfully stored
    /// </summary>
    [JsonPropertyName("stored")]
    public bool Stored { get; set; }
}