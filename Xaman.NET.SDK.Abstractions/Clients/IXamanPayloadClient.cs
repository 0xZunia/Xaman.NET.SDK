using System;
using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.Payload;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the payload client used to create and manage sign requests
/// </summary>
public interface IXamanPayloadClient
{
    /// <summary>
    /// Creates a new sign request (payload)
    /// </summary>
    /// <param name="payload">The payload to create</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created payload response</returns>
    Task<PayloadResponse?> CreateAsync(JsonPayloadRequest payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new sign request (payload)
    /// </summary>
    /// <param name="payload">The payload to create</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created payload response</returns>
    Task<PayloadResponse?> CreateAsync(BlobPayloadRequest payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new sign request (payload)
    /// </summary>
    /// <param name="payload">The transaction payload</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created payload response</returns>
    Task<PayloadResponse?> CreateAsync(TransactionPayload payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a payload by its identifier
    /// </summary>
    /// <param name="payload">The payload response or UUID</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The payload details</returns>
    Task<PayloadDetails?> GetAsync(PayloadResponse payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a payload by its identifier
    /// </summary>
    /// <param name="payloadUuid">The payload UUID</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The payload details</returns>
    Task<PayloadDetails?> GetAsync(string payloadUuid, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a payload by its custom identifier
    /// </summary>
    /// <param name="customIdentifier">The custom identifier</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The payload details</returns>
    Task<PayloadDetails?> GetByCustomIdentifierAsync(string customIdentifier, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Cancels a payload
    /// </summary>
    /// <param name="payload">The payload response</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the cancellation</returns>
    Task<DeletePayloadResponse?> CancelAsync(PayloadResponse payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Cancels a payload
    /// </summary>
    /// <param name="payload">The payload details</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the cancellation</returns>
    Task<DeletePayloadResponse?> CancelAsync(PayloadDetails payload, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Cancels a payload
    /// </summary>
    /// <param name="payloadUuid">The payload UUID</param>
    /// <param name="throwOnError">Whether to throw an exception on error</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the cancellation</returns>
    Task<DeletePayloadResponse?> CancelAsync(string payloadUuid, bool throwOnError = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Subscribes to payload updates
    /// </summary>
    /// <param name="payload">The payload details</param>
    /// <param name="eventHandler">The event handler</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SubscribeAsync(PayloadDetails payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken);
    
    /// <summary>
    /// Subscribes to payload updates
    /// </summary>
    /// <param name="payload">The payload response</param>
    /// <param name="eventHandler">The event handler</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SubscribeAsync(PayloadResponse payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken);
    
    /// <summary>
    /// Subscribes to payload updates
    /// </summary>
    /// <param name="payloadUuid">The payload UUID</param>
    /// <param name="eventHandler">The event handler</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task SubscribeAsync(string payloadUuid, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a payload and subscribes to updates
    /// </summary>
    /// <param name="payload">The payload to create</param>
    /// <param name="eventHandler">The event handler</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created payload response</returns>
    Task<PayloadResponse> CreateAndSubscribeAsync(JsonPayloadRequest payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a payload and subscribes to updates
    /// </summary>
    /// <param name="payload">The payload to create</param>
    /// <param name="eventHandler">The event handler</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created payload response</returns>
    Task<PayloadResponse> CreateAndSubscribeAsync(BlobPayloadRequest payload, EventHandler<PayloadEventArgs> eventHandler, CancellationToken cancellationToken);
}