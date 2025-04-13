using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models;
using Xaman.NET.SDK.Abstractions.Models.XApp;
using Xaman.NET.SDK.Extensions;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Client for xApp-related functionality
/// </summary>
public class XamanXAppClient : IXamanXAppClient
{
    private readonly IXamanHttpClient _httpClient;
    private readonly XamanOptions _options;
    private readonly ILogger<XamanXAppClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanXAppClient"/> class
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="options">The Xaman options</param>
    /// <param name="logger">The logger</param>
    public XamanXAppClient(
        IXamanHttpClient httpClient,
        IOptions<XamanOptions> options,
        ILogger<XamanXAppClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<XAppOttResponse> GetOneTimeTokenDataAsync(string oneTimeToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new XamanValidationException("One-time token cannot be null or empty");
        }
        
        return await _httpClient.GetAsync<XAppOttResponse>($"platform/xapp/ott/{oneTimeToken}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppOttResponse> ReFetchOneTimeTokenDataAsync(string oneTimeToken, string deviceId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new XamanValidationException("One-time token cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            throw new XamanValidationException("Device ID cannot be null or empty");
        }
        
        var hash = $"{oneTimeToken}.{_options.ApiSecret}.{deviceId}".ToUpperInvariant().ToSha1Hash().ToLowerInvariant();
        return await _httpClient.GetAsync<XAppOttResponse>($"platform/xapp/ott/{oneTimeToken}/{hash}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppEventResponse> EventAsync(XAppEventRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.UserToken))
        {
            throw new XamanValidationException("User token cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Body))
        {
            throw new XamanValidationException("Body cannot be null or empty");
        }
        
        return await _httpClient.PostAsync<XAppEventResponse>("platform/xapp/event", request, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppPushResponse> PushAsync(XAppPushRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.UserToken))
        {
            throw new XamanValidationException("User token cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Body))
        {
            throw new XamanValidationException("Body cannot be null or empty");
        }
        
        return await _httpClient.PostAsync<XAppPushResponse>("platform/xapp/push", request, cancellationToken);
    }
}