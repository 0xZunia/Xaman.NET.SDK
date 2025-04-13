using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models.Payload;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Client for creating and managing Xaman payloads (sign requests)
/// </summary>
public class XamanPayloadClient : IXamanPayloadClient
{
    private readonly IXamanHttpClient _httpClient;
    private readonly IXamanWebSocket _webSocket;
    private readonly ILogger<XamanPayloadClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanPayloadClient"/> class
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="webSocket">The WebSocket client</param>
    /// <param name="logger">The logger</param>
    public XamanPayloadClient(
        IXamanHttpClient httpClient,
        IXamanWebSocket webSocket,
        ILogger<XamanPayloadClient> logger)
    {
        _httpClient = httpClient;
        _webSocket = webSocket;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<PayloadResponse?> CreateAsync(JsonPayloadRequest payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.PostAsync<PayloadResponse>("platform/payload", payload, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create payload");
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task<PayloadResponse?> CreateAsync(BlobPayloadRequest payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.PostAsync<PayloadResponse>("platform/payload", payload, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create payload");
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task<PayloadResponse?> CreateAsync(TransactionPayload payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.PostAsync<PayloadResponse>("platform/payload", new { txjson = payload }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create payload");
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task<PayloadDetails?> GetAsync(PayloadResponse payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        return await GetAsync(payload.Uuid, throwOnError, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<PayloadDetails?> GetAsync(string payloadUuid, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.GetAsync<PayloadDetails>($"platform/payload/{payloadUuid}", cancellationToken);
        }
        catch (Exception ex) when (ex is not XamanPayloadNotFoundException)
        {
            _logger.LogError(ex, "Failed to get payload {PayloadUuid}", payloadUuid);
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task<PayloadDetails?> GetByCustomIdentifierAsync(string customIdentifier, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.GetAsync<PayloadDetails>($"platform/payload/ci/{customIdentifier}", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get payload by custom identifier {CustomIdentifier}", customIdentifier);
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task<DeletePayloadResponse?> CancelAsync(PayloadResponse payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        return await CancelAsync(payload.Uuid, throwOnError, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<DeletePayloadResponse?> CancelAsync(PayloadDetails payload, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        return await CancelAsync(payload.Meta.Uuid, throwOnError, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<DeletePayloadResponse?> CancelAsync(string payloadUuid, bool throwOnError = false, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _httpClient.DeleteAsync<DeletePayloadResponse>($"platform/payload/{payloadUuid}", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to cancel payload {PayloadUuid}", payloadUuid);
            
            if (throwOnError)
            {
                throw;
            }
            
            return null;
        }
    }
    
    /// <inheritdoc />
    public async Task SubscribeAsync(PayloadDetails payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        await SubscribeAsync(payload.Meta.Uuid, eventHandler, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task SubscribeAsync(PayloadResponse payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        await SubscribeAsync(payload.Uuid, eventHandler, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task SubscribeAsync(string payloadUuid, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        using var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        // A small delay to ensure the payload has been distributed across the Xaman backend
        await Task.Delay(75, cancellationToken);

        var payload = await GetAsync(payloadUuid, true, cancellationToken);

        if (payload == null)
        {
            throw new XamanPayloadNotFoundException(payloadUuid);
        }

        await foreach (var message in _webSocket.SubscribeAsync(payloadUuid, source.Token))
        {
            var data = JsonDocument.Parse(message);

            eventHandler(this, new PayloadEventArgs
            {
                Uuid = payloadUuid,
                Data = data,
                Payload = payload,
                CloseConnectionAsync = () => source.Cancel()
            });
        }
    }
    
    /// <inheritdoc />
    public async Task<PayloadResponse> CreateAndSubscribeAsync(JsonPayloadRequest payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        var result = await CreateAsync(payload, true, cancellationToken);
        
        if (result == null)
        {
            throw new XamanApiException(500, "Failed to create payload");
        }
        
        await SubscribeAsync(result.Uuid, eventHandler, cancellationToken);
        return result;
    }
    
    /// <inheritdoc />
    public async Task<PayloadResponse> CreateAndSubscribeAsync(BlobPayloadRequest payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken)
    {
        var result = await CreateAsync(payload, true, cancellationToken);
        
        if (result == null)
        {
            throw new XamanApiException(500, "Failed to create payload");
        }
        
        await SubscribeAsync(result.Uuid, eventHandler, cancellationToken);
        return result;
    }
}