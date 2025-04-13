using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Enums;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models.Misc;
using Xaman.NET.SDK.Extensions;
using Xaman.NET.SDK.Helpers;

namespace Xaman.NET.SDK.Clients;

/// <summary>
/// Client for miscellaneous Xaman API endpoints
/// </summary>
public class XamanMiscClient : IXamanMiscClient
{
    private const int MinimumAvatarDimensions = 200;
    private readonly IXamanHttpClient _httpClient;
    private readonly ILogger<XamanMiscClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanMiscClient"/> class
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    /// <param name="logger">The logger</param>
    public XamanMiscClient(
        IXamanHttpClient httpClient,
        ILogger<XamanMiscClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<PingResponse> GetPingAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<PingResponse>("platform/ping", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<CuratedAssetsResponse> GetCuratedAssetsAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<CuratedAssetsResponse>("platform/curated-assets", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<HookInfoResponse> GetHookInfoAsync(string hookHash, CancellationToken cancellationToken = default)
    {
        if (!hookHash.IsSHA512H())
        {
            throw new XamanValidationException("Invalid Hook Hash (expecting SHA-512Half)");
        }
        
        var result = await _httpClient.GetAsync<HookInfo>($"platform/hookhash/{hookHash}", cancellationToken);
        return new HookInfoResponse(hookHash, result);
    }
    
    /// <inheritdoc />
    public async Task<List<HookInfoResponse>> GetAllHookInfosAsync(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetAsync<Dictionary<string, HookInfo>>("platform/hookhash", cancellationToken);
        return result.Select(x => new HookInfoResponse(x.Key, x.Value)).ToList();
    }
    
    /// <inheritdoc />
    public async Task<List<RailsResponse>> GetRailsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetAsync<Dictionary<string, RailsNetwork>>("platform/rails", cancellationToken);
        return result.Select(x => new RailsResponse(x.Key, x.Value)).ToList();
    }
    
    /// <inheritdoc />
    public async Task<TransactionResponse> GetTransactionAsync(string txHash, CancellationToken cancellationToken = default)
    {
        if (!txHash.IsSHA512H())
        {
            throw new XamanValidationException("Invalid Transaction Hash (expecting SHA-512Half)");
        }
        
        return await _httpClient.GetAsync<TransactionResponse>($"platform/xrpl-tx/{txHash}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<KycStatus> GetKycStatusAsync(string userTokenOrAccount, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userTokenOrAccount))
        {
            throw new XamanValidationException("User token or account cannot be null or empty");
        }
        
        if (userTokenOrAccount.IsAccountAddress())
        {
            var kycInfo = await _httpClient.GetPublicAsync<XamanKycInfo>($"platform/kyc-status/{userTokenOrAccount}", cancellationToken);
            return kycInfo.KycApproved ? KycStatus.Successful : KycStatus.None;
        }
        
        if (userTokenOrAccount.IsValidUuid())
        {
            var request = new XamanKycStatusRequest
            {
                UserToken = userTokenOrAccount
            };
            
            var kycInfo = await _httpClient.PostAsync<XamanKycStatusInfo>("platform/kyc-status", request, cancellationToken);
            return EnumHelper.GetValueFromName<KycStatus>(kycInfo.KycStatus);
        }
        
        throw new XamanValidationException("Invalid user token or account provided");
    }
    
    /// <inheritdoc />
    public async Task<RatesResponse> GetRatesAsync(string currencyCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new XamanValidationException("Currency code cannot be null or empty");
        }
        
        return await _httpClient.GetAsync<RatesResponse>($"platform/rates/{currencyCode.Trim().ToUpperInvariant()}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<UserTokensResponse> VerifyUserTokenAsync(string userToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userToken))
        {
            throw new XamanValidationException("User token cannot be null or empty");
        }
        
        return await _httpClient.GetAsync<UserTokensResponse>($"platform/user-token/{userToken}", cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<UserTokensResponse> VerifyUserTokensAsync(string[] userTokens, CancellationToken cancellationToken = default)
    {
        if (userTokens == null || userTokens.Length == 0)
        {
            throw new XamanValidationException("User tokens cannot be null or empty");
        }
        
        var request = new UserTokensRequest
        {
            Tokens = new List<string>(userTokens)
        };
        
        return await _httpClient.PostAsync<UserTokensResponse>("platform/user-tokens", request, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<AccountMetaResponse> GetAccountMetaAsync(string account, CancellationToken cancellationToken = default)
    {
        if (!account.IsAccountAddress())
        {
            throw new XamanValidationException("Value should be a valid account address");
        }
        
        return await _httpClient.GetAsync<AccountMetaResponse>($"platform/account-meta/{account}", cancellationToken);
    }
    
    /// <inheritdoc />
    public string GetAvatarUrl(string account, int dimensions, int padding = 0)
    {
        if (string.IsNullOrWhiteSpace(account))
        {
            throw new XamanValidationException("Account cannot be null or empty");
        }
        
        if (dimensions < MinimumAvatarDimensions)
        {
            throw new ArgumentOutOfRangeException(nameof(dimensions),
                $"The minimum (square) dimensions are {MinimumAvatarDimensions}.");
        }
        
        if (padding < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(padding), "The padding should be equal or greater than zero.");
        }
        
        return $"https://xaman.app/avatar/{account}_{dimensions}_{padding}.png";
    }
}