using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the HTTP client used to communicate with the Xaman API
/// </summary>
public interface IXamanHttpClient
{
    /// <summary>
    /// Sends a GET request to the specified endpoint
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a GET request to the specified endpoint using the provided HttpClient
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="client">The HttpClient to use</param>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> GetAsync<T>(HttpClient client, string endpoint, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a GET request to the specified endpoint without API credentials
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> GetPublicAsync<T>(string endpoint, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a POST request to the specified endpoint
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="content">The content to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> PostAsync<T>(string endpoint, object content, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a POST request to the specified endpoint
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="json">The JSON content to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> PostAsync<T>(string endpoint, string json, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a POST request to the specified endpoint using the provided HttpClient
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="client">The HttpClient to use</param>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="json">The JSON content to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> PostAsync<T>(HttpClient client, string endpoint, string json, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a DELETE request to the specified endpoint
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sends a DELETE request to the specified endpoint using the provided HttpClient
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="client">The HttpClient to use</param>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> DeleteAsync<T>(HttpClient client, string endpoint, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a configured HttpClient instance
    /// </summary>
    /// <param name="setCredentials">Whether to include API credentials in the request headers</param>
    /// <returns>A configured HttpClient instance</returns>
    HttpClient GetHttpClient(bool setCredentials = true);
}