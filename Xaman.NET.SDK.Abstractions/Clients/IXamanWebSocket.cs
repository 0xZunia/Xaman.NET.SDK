using System.Collections.Generic;
using System.Threading;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the WebSocket client used to subscribe to payload updates
/// </summary>
public interface IXamanWebSocket
{
    /// <summary>
    /// Subscribes to payload updates via WebSocket
    /// </summary>
    /// <param name="payloadUuid">The payload UUID to subscribe to</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Async enumerable of WebSocket messages</returns>
    IAsyncEnumerable<string> SubscribeAsync(string payloadUuid, CancellationToken cancellationToken);
}