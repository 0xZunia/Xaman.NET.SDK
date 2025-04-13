using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models.XAppJwt;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Client for JWT-based xApp authentication
/// </summary>
public class XamanXAppJwtClient : IXamanXAppJwtClient
{
    private readonly IXamanHttpClient _httpClient;
    private readonly ILogger<XamanXAppJwtClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanXAppJwtClient"/> class
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="logger">The logger</param>
    public XamanXAppJwtClient(
        IXamanHttpClient httpClient,
        ILogger<XamanXAppJwtClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<XAppJwtAuthorizeResponse> AuthorizeAsync(string oneTimeToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(oneTimeToken))
        {
            throw new XamanValidationException("One-time token cannot be null or empty");
        }
        
        var httpClient = _httpClient.GetHttpClient(true);
        httpClient.DefaultRequestHeaders.Add("X-API-OTT", oneTimeToken);
        
        // Utilisez GetAsync avec HttpClient et endpoint
        return await _httpClient.GetAsync<XAppJwtAuthorizeResponse>(httpClient, "xapp-jwt/authorize", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppJwtUserDataResponse> GetUserDataAsync(string jwt, string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jwt))
        {
            throw new XamanValidationException("JWT cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new XamanValidationException("Key cannot be null or empty");
        }
        
        var httpClient = _httpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
        
        // Utilisez GetAsync avec HttpClient et endpoint
        return await _httpClient.GetAsync<XAppJwtUserDataResponse>(httpClient, $"xapp-jwt/userdata/{key}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppJwtUserDataUpdateResponse> SetUserDataAsync(string jwt, string key, string json, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jwt))
        {
            throw new XamanValidationException("JWT cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new XamanValidationException("Key cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new XamanValidationException("JSON cannot be null or empty");
        }
        
        var httpClient = _httpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
        
        // Créez d'abord le client HTTP avec les en-têtes nécessaires
        // Puis utilisez le client avec l'endpoint et le contenu JSON
        return await _httpClient.PostAsync<XAppJwtUserDataUpdateResponse>(httpClient, $"xapp-jwt/userdata/{key}", json, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppJwtUserDataUpdateResponse> DeleteUserDataAsync(string jwt, string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jwt))
        {
            throw new XamanValidationException("JWT cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new XamanValidationException("Key cannot be null or empty");
        }
        
        var httpClient = _httpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
        
        // Utilisez DeleteAsync avec HttpClient et endpoint
        return await _httpClient.DeleteAsync<XAppJwtUserDataUpdateResponse>(httpClient, $"xapp-jwt/userdata/{key}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<XAppJwtNFTokenDetail> GetNFTokenDetailAsync(string jwt, string tokenId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jwt))
        {
            throw new XamanValidationException("JWT cannot be null or empty");
        }
        
        if (string.IsNullOrWhiteSpace(tokenId))
        {
            throw new XamanValidationException("Token ID cannot be null or empty");
        }
        
        var httpClient = _httpClient.GetHttpClient(false);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
        
        // Utilisez GetAsync avec HttpClient et endpoint
        return await _httpClient.GetAsync<XAppJwtNFTokenDetail>(httpClient, $"xapp-jwt/nftoken-detail/{tokenId}", cancellationToken);
    }
}