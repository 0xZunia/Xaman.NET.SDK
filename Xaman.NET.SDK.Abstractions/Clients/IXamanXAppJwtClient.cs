using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.XAppJwt;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the xApp JWT client that provides JWT authentication for xApps
/// </summary>
public interface IXamanXAppJwtClient
{
    /// <summary>
    /// Authorizes with a one-time token to obtain a JWT
    /// </summary>
    /// <param name="oneTimeToken">The one-time token provided when Xaman launches the xApp</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authorization response with JWT</returns>
    Task<XAppJwtAuthorizeResponse> AuthorizeAsync(string oneTimeToken, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets user data from the JWT store
    /// </summary>
    /// <param name="jwt">The JWT token</param>
    /// <param name="key">Key to retrieve (comma-separated for multiple keys)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User data response</returns>
    Task<XAppJwtUserDataResponse> GetUserDataAsync(string jwt, string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Sets user data in the JWT store
    /// </summary>
    /// <param name="jwt">The JWT token</param>
    /// <param name="key">Key to store</param>
    /// <param name="json">JSON data to store</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response indicating success</returns>
    Task<XAppJwtUserDataUpdateResponse> SetUserDataAsync(string jwt, string key, string json, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes user data from the JWT store
    /// </summary>
    /// <param name="jwt">The JWT token</param>
    /// <param name="key">Key to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response indicating success</returns>
    Task<XAppJwtUserDataUpdateResponse> DeleteUserDataAsync(string jwt, string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets NFT token details
    /// </summary>
    /// <param name="jwt">The JWT token</param>
    /// <param name="tokenId">The NFT token ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>NFT token details</returns>
    Task<XAppJwtNFTokenDetail> GetNFTokenDetailAsync(string jwt, string tokenId, CancellationToken cancellationToken = default);
}