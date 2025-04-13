using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;

namespace Xaman.NET.SDK.WebSocket;

/// <summary>
/// WebSocket client for the Xaman API
/// </summary>
public class XamanWebSocket : IXamanWebSocket
{
    private readonly ILogger<XamanWebSocket> _logger;
    private string _payloadUuid = default!;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanWebSocket"/> class
    /// </summary>
    /// <param name="logger">The logger</param>
    public XamanWebSocket(ILogger<XamanWebSocket> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async IAsyncEnumerable<string> SubscribeAsync(string payloadUuid, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        _payloadUuid = payloadUuid;
        
        using var webSocket = new ClientWebSocket();
        try
        {
            await webSocket.ConnectAsync(new Uri($"wss://xaman.app/sign/{_payloadUuid}"), CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to WebSocket for payload {PayloadUuid}", _payloadUuid);
            throw new XamanWebSocketException($"Failed to connect to WebSocket for payload {_payloadUuid}", ex);
        }
        
        if (webSocket.State == WebSocketState.Open)
        {
            _logger.LogInformation("Payload {PayloadUuid}: Subscription active (WebSocket opened)", _payloadUuid);
            
            var buffer = new ArraySegment<byte>(new byte[4096]);
            
            while (webSocket.State == WebSocketState.Open)
            {
                using var ms = new MemoryStream();
                WebSocketReceiveResult? result;
                
                try
                {
                    do
                    {
                        result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                        await ms.WriteAsync(buffer.Array!, buffer.Offset, result.Count, cancellationToken);
                    } while (!result.EndOfMessage && !cancellationToken.IsCancellationRequested);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Payload {PayloadUuid}: Subscription ended (WebSocket closed)", _payloadUuid);
                    yield break;
                }
                catch (WebSocketException ex)
                {
                    _logger.LogError(ex, "WebSocket error for payload {PayloadUuid}", _payloadUuid);
                    throw new XamanWebSocketException($"WebSocket error for payload {_payloadUuid}", ex);
                }
                
                ms.Seek(0, SeekOrigin.Begin);
                var messageBytes = ms.ToArray();
                if (messageBytes.Length > 0)
                {
                    var message = Encoding.UTF8.GetString(messageBytes);
                    _logger.LogDebug("Received WebSocket message: {Message}", message);
                    yield return message;
                }
                
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _logger.LogInformation("Payload {PayloadUuid}: WebSocket closed by server", _payloadUuid);
                    break;
                }
            }
        }
        else
        {
            _logger.LogWarning("WebSocket connection not open for payload {PayloadUuid}. State: {State}", _payloadUuid, webSocket.State);
            throw new XamanWebSocketException($"WebSocket connection not open for payload {_payloadUuid}. State: {webSocket.State}");
        }
    }
}