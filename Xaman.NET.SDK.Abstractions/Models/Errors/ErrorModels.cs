using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Errors;

/// <summary>
/// API error response
/// </summary>
public class XamanApiError
{
    /// <summary>
    /// Error details
    /// </summary>
    [JsonPropertyName("error")]
    public XamanApiErrorDetails Error { get; set; } = default!;
}

/// <summary>
/// API error details
/// </summary>
public class XamanApiErrorDetails
{
    /// <summary>
    /// Error reference for support
    /// </summary>
    [JsonPropertyName("reference")]
    public string? Reference { get; set; }
    
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public int? Code { get; set; }
}

/// <summary>
/// Fatal API error response
/// </summary>
public class XamanFatalApiError
{
    /// <summary>
    /// Indicates if this is an error
    /// </summary>
    [JsonPropertyName("error")]
    public bool Error { get; set; }
    
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;
    
    /// <summary>
    /// Error reference for support
    /// </summary>
    [JsonPropertyName("reference")]
    public string? Reference { get; set; }
    
    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
    
    /// <summary>
    /// The request that caused the error
    /// </summary>
    [JsonPropertyName("req")]
    public string? Request { get; set; }
    
    /// <summary>
    /// The HTTP method of the request
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; set; }
}