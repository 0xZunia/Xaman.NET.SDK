using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.Storage;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the user store client that provides persistent storage for user data
/// </summary>
public interface IXamanUserStoreClient
{
    /// <summary>
    /// Retrieves stored data for the application
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Storage response with application data</returns>
    Task<StorageResponse> GetAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Stores data for the application
    /// </summary>
    /// <param name="json">JSON data to store</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Storage store response indicating success</returns>
    Task<StorageStoreResponse> StoreAsync(string json, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Clears all stored data for the application
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Storage store response indicating success</returns>
    Task<StorageStoreResponse> ClearAsync(CancellationToken cancellationToken = default);
}