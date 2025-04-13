using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.XApp;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the xApp client that provides functionality for xApp development
/// </summary>
public interface IXamanXAppClient
{
    /// <summary>
    /// Gets the one-time token data when a user opens an xApp
    /// </summary>
    /// <param name="oneTimeToken">The one-time token provided when Xaman launches the xApp</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>One-time token response with user and context data</returns>
    Task<XAppOttResponse> GetOneTimeTokenDataAsync(string oneTimeToken, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Re-fetches one-time token data during xApp development
    /// </summary>
    /// <param name="oneTimeToken">The one-time token</param>
    /// <param name="deviceId">The device ID that initially retrieved the token data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>One-time token response with user and context data</returns>
    Task<XAppOttResponse> ReFetchOneTimeTokenDataAsync(string oneTimeToken, string deviceId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends an event to a user's Xaman app, which appears in their event list and sends a push notification
    /// </summary>
    /// <param name="request">The event request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Event response</returns>
    Task<XAppEventResponse> EventAsync(XAppEventRequest request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a push notification to a user's Xaman app
    /// </summary>
    /// <param name="request">The push notification request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Push notification response</returns>
    Task<XAppPushResponse> PushAsync(XAppPushRequest request, CancellationToken cancellationToken = default);
}