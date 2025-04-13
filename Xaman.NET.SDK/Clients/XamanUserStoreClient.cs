using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Models.Storage;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Client for managing application storage in Xaman
/// </summary>
public class XamanUserStoreClient : IXamanUserStoreClient
{
    private readonly IXamanHttpClient _httpClient;
    private readonly ILogger<XamanUserStoreClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanUserStoreClient"/> class
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="logger">The logger</param>
    public XamanUserStoreClient(
        IXamanHttpClient httpClient,
        ILogger<XamanUserStoreClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<StorageResponse> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<StorageResponse>("platform/app-storage", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<StorageStoreResponse> StoreAsync(string json, CancellationToken cancellationToken = default)
    {
        return await _httpClient.PostAsync<StorageStoreResponse>("platform/app-storage", json, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<StorageStoreResponse> ClearAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.DeleteAsync<StorageStoreResponse>("platform/app-storage", cancellationToken);
    }
}