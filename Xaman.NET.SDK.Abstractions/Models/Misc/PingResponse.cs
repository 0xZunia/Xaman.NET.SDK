using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Misc;

/// <summary>
/// Response from the Xaman API ping endpoint
/// </summary>
public class PingResponse
{
    /// <summary>
    /// Indicates that the API is responding
    /// </summary>
    [JsonPropertyName("pong")]
    public bool Pong { get; set; }
    
    /// <summary>
    /// Authentication and application details
    /// </summary>
    [JsonPropertyName("auth")]
    public XamanAuth Auth { get; set; } = default!;
}

/// <summary>
/// Authentication information for the Xaman API
/// </summary>
public class XamanAuth
{
    /// <summary>
    /// Application details
    /// </summary>
    [JsonPropertyName("application")]
    public XamanApplication Application { get; set; } = default!;
    
    /// <summary>
    /// Call details
    /// </summary>
    [JsonPropertyName("call")]
    public XamanCall Call { get; set; } = default!;
    
    /// <summary>
    /// Quota information
    /// </summary>
    [JsonPropertyName("quota")]
    public Dictionary<string, object> Quota { get; set; } = default!;
}

/// <summary>
/// Application information
/// </summary>
public class XamanApplication
{
    /// <summary>
    /// The UUID of the application
    /// </summary>
    [JsonPropertyName("uuidv4")]
    public string Uuidv4 { get; set; } = default!;
    
    /// <summary>
    /// The name of the application
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The description of the application
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// The webhook URL of the application
    /// </summary>
    [JsonPropertyName("webhookurl")]
    public string? WebhookUrl { get; set; }
    
    /// <summary>
    /// The redirect URIs of the application
    /// </summary>
    [JsonPropertyName("redirecturis")]
    public List<string> RedirectUris { get; set; } = new();
    
    /// <summary>
    /// Indicates if the application is disabled
    /// </summary>
    [JsonPropertyName("disabled")]
    public int Disabled { get; set; }
    
    /// <summary>
    /// The icon URL of the application
    /// </summary>
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; set; }
    
    /// <summary>
    /// The issued user token from a previous sign request
    /// </summary>
    [JsonPropertyName("issued_user_token")]
    public object? IssuedUserToken { get; set; }
}

/// <summary>
/// Call information
/// </summary>
public class XamanCall
{
    /// <summary>
    /// The UUID of the call
    /// </summary>
    [JsonPropertyName("uuidv4")]
    public string Uuidv4 { get; set; } = default!;
}