using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Xaman.NET.SDK.Abstractions.Models.Payload;

/// <summary>
/// Event arguments for payload events
/// </summary>
public class PayloadEventArgs : EventArgs
{
    /// <summary>
    /// The UUID of the payload
    /// </summary>
    public string Uuid { get; set; } = default!;
    
    /// <summary>
    /// The data from the event
    /// </summary>
    public JsonDocument Data { get; set; } = default!;
    
    /// <summary>
    /// The payload details
    /// </summary>
    public PayloadDetails Payload { get; set; } = default!;
    
    /// <summary>
    /// Action to close the connection
    /// </summary>
    public Action CloseConnectionAsync { get; set; } = default!;
}

/// <summary>
/// Payload subscription for WebSocket updates
/// </summary>
public class PayloadSubscription
{
    /// <summary>
    /// The payload details
    /// </summary>
    public PayloadDetails Payload { get; set; } = default!;
    
    /// <summary>
    /// Promise for the resolved data
    /// </summary>
    public Task<object?> Resolved { get; set; } = default!;
    
    /// <summary>
    /// Action to resolve the subscription
    /// </summary>
    public Action<object?> Resolve { get; set; } = default!;
    
    /// <summary>
    /// The WebSocket client
    /// </summary>
    public System.Net.WebSockets.ClientWebSocket WebSocket { get; set; } = default!;
}

/// <summary>
/// Payload subscription with created payload
/// </summary>
public class PayloadAndSubscription
{
    /// <summary>
    /// The created payload
    /// </summary>
    public PayloadResponse Created { get; set; } = default!;
    
    /// <summary>
    /// The payload subscription
    /// </summary>
    public PayloadSubscription Subscription { get; set; } = default!;
    
    /// <summary>
    /// Promise for the resolved data
    /// </summary>
    public Task<object?> Resolved => Subscription.Resolved;
}