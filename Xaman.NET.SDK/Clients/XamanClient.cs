using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Models.Misc;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Main client for interacting with the Xaman API
/// </summary>
public class XamanClient : IXamanClient
{
    private readonly IXamanHttpClient _httpClient;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanClient"/> class
    /// </summary>
    public XamanClient(
        IXamanHttpClient httpClient,
        IXamanPayloadClient payloadClient,
        IXamanMiscClient miscClient,
        IXamanXAppClient xAppClient,
        IXamanUserStoreClient userStoreClient,
        IXrplClient xrplClient)
    {
        _httpClient = httpClient;
        Payload = payloadClient;
        Misc = miscClient;
        XApp = xAppClient;
        UserStore = userStoreClient;
        Xrpl = xrplClient;
    }
    
    /// <inheritdoc />
    public IXamanPayloadClient Payload { get; }
    
    /// <inheritdoc />
    public IXamanMiscClient Misc { get; }
    
    /// <inheritdoc />
    public IXamanXAppClient XApp { get; }
    
    /// <inheritdoc />
    public IXamanUserStoreClient UserStore { get; }
    
    /// <inheritdoc />
    public IXrplClient Xrpl { get; }
    
    /// <inheritdoc />
    public async Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<PingResponse>("platform/ping", cancellationToken);
    }
}