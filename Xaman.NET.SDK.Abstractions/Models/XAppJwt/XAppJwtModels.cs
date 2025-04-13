using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.XAppJwt;

/// <summary>
/// Response from JWT authorization
/// </summary>
public class XAppJwtAuthorizeResponse
{
    /// <summary>
    /// The one-time token data
    /// </summary>
    [JsonPropertyName("ott")]
    public XApp.XAppOttResponse OTT { get; set; } = default!;
    
    /// <summary>
    /// The application information
    /// </summary>
    [JsonPropertyName("app")]
    public XAppJwtAppResponse App { get; set; } = default!;
    
    /// <summary>
    /// The JWT token
    /// </summary>
    [JsonPropertyName("jwt")]
    public string JWT { get; set; } = default!;
}

/// <summary>
/// Application information for JWT
/// </summary>
public class XAppJwtAppResponse
{
    /// <summary>
    /// The name of the application
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}

/// <summary>
/// Response from user data operations
/// </summary>
public class XAppJwtUserDataResponse
{
    /// <summary>
    /// The operation performed
    /// </summary>
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = default!;
    
    /// <summary>
    /// The data retrieved
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, JsonDocument> Data { get; set; } = default!;
    
    /// <summary>
    /// The keys available
    /// </summary>
    [JsonPropertyName("keys")]
    public List<string> Keys { get; set; } = default!;
    
    /// <summary>
    /// The number of keys
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

/// <summary>
/// Response from updating user data
/// </summary>
public class XAppJwtUserDataUpdateResponse
{
    /// <summary>
    /// The operation performed
    /// </summary>
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the data was persisted
    /// </summary>
    [JsonPropertyName("persisted")]
    public bool Persisted { get; set; }
}

/// <summary>
/// NFT token details
/// </summary>
public class XAppJwtNFTokenDetail
{
    /// <summary>
    /// The issuer of the token
    /// </summary>
    [JsonPropertyName("issuer")]
    public string? Issuer { get; set; }
    
    /// <summary>
    /// The token identifier
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    
    /// <summary>
    /// The owner of the token
    /// </summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }
    
    /// <summary>
    /// The name of the token
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// The image URL of the token
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}