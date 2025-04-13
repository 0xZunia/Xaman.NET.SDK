using System;

namespace Xaman.NET.SDK.Abstractions.Models;

/// <summary>
/// Configuration options for the Xaman SDK
/// </summary>
public class XamanOptions
{
    /// <summary>
    /// The base URL of the Xaman API
    /// </summary>
    public string RestClientAddress { get; set; } = "https://xaman.app/api/v1";
    
    /// <summary>
    /// The API key for the Xaman API
    /// </summary>
    public string ApiKey { get; set; } = default!;
    
    /// <summary>
    /// The API secret for the Xaman API
    /// </summary>
    public string ApiSecret { get; set; } = default!;
    
    /// <summary>
    /// The timeout for HTTP requests
    /// </summary>
    public TimeSpan HttpTimeout { get; set; } = TimeSpan.FromSeconds(30);
    
    /// <summary>
    /// The number of retries for HTTP requests
    /// </summary>
    public int MaxRetries { get; set; } = 3;
    
    /// <summary>
    /// The delay between retries for HTTP requests
    /// </summary>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);
    
    /// <summary>
    /// Validates the API key
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the API key is invalid</exception>
    public void ValidateApiKey()
    {
        if (string.IsNullOrWhiteSpace(ApiKey) || !IsValidUuid(ApiKey))
        {
            throw new ArgumentException("Invalid API Key. Must be a valid UUID.");
        }
    }
    
    /// <summary>
    /// Validates the API secret
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the API secret is invalid</exception>
    public void ValidateApiSecret()
    {
        if (string.IsNullOrWhiteSpace(ApiSecret) || !IsValidUuid(ApiSecret))
        {
            throw new ArgumentException("Invalid API Secret. Must be a valid UUID.");
        }
    }
    
    private static bool IsValidUuid(string value)
    {
        return Guid.TryParse(value, out _);
    }
}