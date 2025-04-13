using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Enums;
using Xaman.NET.SDK.Abstractions.Models.Misc;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Interface for the misc client that provides various utility functions
/// </summary>
public interface IXamanMiscClient
{
    /// <summary>
    /// Pings the Xaman API to verify the connection and credentials
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ping response with application details</returns>
    Task<PingResponse> GetPingAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the curated assets (trusted issuers and assets)
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Curated assets information</returns>
    Task<CuratedAssetsResponse> GetCuratedAssetsAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets information about a specific Hook hash
    /// </summary>
    /// <param name="hookHash">The SHA-512Half hash of the Hook</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Hook information</returns>
    Task<HookInfoResponse> GetHookInfoAsync(string hookHash, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets information about all Hooks known to Xaman
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of Hook information responses</returns>
    Task<List<HookInfoResponse>> GetAllHookInfosAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets information about all networks known to Xaman
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of network information responses</returns>
    Task<List<RailsResponse>> GetRailsAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets transaction details and balance changes
    /// </summary>
    /// <param name="txHash">The SHA-512Half transaction hash</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Transaction details</returns>
    Task<TransactionResponse> GetTransactionAsync(string txHash, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the KYC status for a user token or account
    /// </summary>
    /// <param name="userTokenOrAccount">The user token or account address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>KYC status</returns>
    Task<KycStatus> GetKycStatusAsync(string userTokenOrAccount, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets exchange rates for a specified currency
    /// </summary>
    /// <param name="currencyCode">The 3-letter ISO currency code</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exchange rate information</returns>
    Task<RatesResponse> GetRatesAsync(string currencyCode, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Verifies a user token for validity
    /// </summary>
    /// <param name="userToken">The user token to verify</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User token validation response</returns>
    Task<UserTokensResponse> VerifyUserTokenAsync(string userToken, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Verifies multiple user tokens for validity
    /// </summary>
    /// <param name="userTokens">The user tokens to verify</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User tokens validation response</returns>
    Task<UserTokensResponse> VerifyUserTokensAsync(string[] userTokens, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets account metadata for an XRPL account
    /// </summary>
    /// <param name="account">The XRPL account address</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Account metadata response</returns>
    Task<AccountMetaResponse> GetAccountMetaAsync(string account, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the URL for an account avatar
    /// </summary>
    /// <param name="account">The XRPL account address</param>
    /// <param name="dimensions">The square dimensions of the avatar</param>
    /// <param name="padding">Optional padding around the avatar</param>
    /// <returns>The URL to the avatar image</returns>
    string GetAvatarUrl(string account, int dimensions, int padding = 0);
}