using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.Misc;

#region KYC Models

/// <summary>
/// KYC status information for a user
/// </summary>
public class XamanKycInfo
{
    /// <summary>
    /// The account address
    /// </summary>
    [JsonPropertyName("account")]
    public string Account { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the account has completed KYC
    /// </summary>
    [JsonPropertyName("kycApproved")]
    public bool KycApproved { get; set; }
}

/// <summary>
/// KYC status information for a user token
/// </summary>
public class XamanKycStatusInfo
{
    /// <summary>
    /// The KYC status
    /// </summary>
    [JsonPropertyName("kycStatus")]
    public string KycStatus { get; set; } = default!;
}

/// <summary>
/// KYC status request
/// </summary>
public class XamanKycStatusRequest
{
    /// <summary>
    /// The user token to check
    /// </summary>
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;
}

#endregion

#region User Token Models

/// <summary>
/// User tokens response
/// </summary>
public class UserTokensResponse
{
    /// <summary>
    /// The list of user token validities
    /// </summary>
    [JsonPropertyName("tokens")]
    public List<UserTokenValidity> Tokens { get; set; } = default!;
}

/// <summary>
/// User token validity information
/// </summary>
public class UserTokenValidity
{
    /// <summary>
    /// The user token
    /// </summary>
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the token is active
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }
    
    /// <summary>
    /// When the token was issued (UNIX timestamp)
    /// </summary>
    [JsonPropertyName("issued")]
    public int? Issued { get; set; }
    
    /// <summary>
    /// When the token expires (UNIX timestamp)
    /// </summary>
    [JsonPropertyName("expires")]
    public int? Expires { get; set; }
}

/// <summary>
/// User tokens request
/// </summary>
public class UserTokensRequest
{
    /// <summary>
    /// The user tokens to check
    /// </summary>
    [JsonPropertyName("tokens")]
    public List<string> Tokens { get; set; } = default!;
}

#endregion

#region Rates Models

/// <summary>
/// Exchange rates response
/// </summary>
public class RatesResponse
{
    /// <summary>
    /// The USD rate
    /// </summary>
    [JsonPropertyName("USD")]
    public double USD { get; set; }
    
    /// <summary>
    /// The XRP rate
    /// </summary>
    [JsonPropertyName("XRP")]
    public double XRP { get; set; }
    
    /// <summary>
    /// The metadata
    /// </summary>
    [JsonPropertyName("__meta")]
    public RatesMeta Meta { get; set; } = default!;
}

/// <summary>
/// Exchange rates metadata
/// </summary>
public class RatesMeta
{
    /// <summary>
    /// The currency information
    /// </summary>
    [JsonPropertyName("currency")]
    public RatesCurrency Currency { get; set; } = default!;
}

/// <summary>
/// Currency information for rates
/// </summary>
public class RatesCurrency
{
    /// <summary>
    /// The English name of the currency
    /// </summary>
    [JsonPropertyName("en")]
    public string En { get; set; } = default!;
    
    /// <summary>
    /// The currency code
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// The currency symbol
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }
    
    /// <summary>
    /// The number of decimal places for ISO display
    /// </summary>
    [JsonPropertyName("isoDecimals")]
    public int IsoDecimals { get; set; }
}

#endregion

#region Transaction Models

/// <summary>
/// Transaction response
/// </summary>
public class TransactionResponse
{
    /// <summary>
    /// The transaction ID
    /// </summary>
    [JsonPropertyName("txid")]
    public string Txid { get; set; } = default!;
    
    /// <summary>
    /// The balance changes
    /// </summary>
    [JsonPropertyName("balanceChanges")]
    public Dictionary<string, List<TransactionBalanceChange>> BalanceChanges { get; set; } = default!;
    
    /// <summary>
    /// The node that processed the transaction
    /// </summary>
    [JsonPropertyName("node")]
    public string Node { get; set; } = default!;
    
    /// <summary>
    /// The transaction details
    /// </summary>
    [JsonPropertyName("transaction")]
    public JsonDocument? Transaction { get; set; }
}

/// <summary>
/// Balance change information
/// </summary>
public class TransactionBalanceChange
{
    /// <summary>
    /// The counterparty account
    /// </summary>
    [JsonPropertyName("counterparty")]
    public string CounterParty { get; set; } = default!;
    
    /// <summary>
    /// The currency
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;
    
    /// <summary>
    /// The value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;
    
    /// <summary>
    /// The formatted value
    /// </summary>
    [JsonPropertyName("formatted")]
    public TransactionBalanceChangeFormatted Formatted { get; set; } = default!;
}

/// <summary>
/// Formatted balance change
/// </summary>
public class TransactionBalanceChangeFormatted
{
    /// <summary>
    /// The formatted value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;
    
    /// <summary>
    /// The formatted currency
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;
}

#endregion

#region Curated Assets Models

/// <summary>
/// Curated assets response
/// </summary>
public class CuratedAssetsResponse
{
    /// <summary>
    /// The list of issuers
    /// </summary>
    [JsonPropertyName("issuers")]
    public List<string> Issuers { get; set; } = default!;
    
    /// <summary>
    /// The list of currencies
    /// </summary>
    [JsonPropertyName("currencies")]
    public List<string> Currencies { get; set; } = default!;
    
    /// <summary>
    /// The details of the curated assets
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, CuratedAssetsDetails> Details { get; set; } = default!;
}

/// <summary>
/// Curated assets details
/// </summary>
public class CuratedAssetsDetails
{
    /// <summary>
    /// The ID of the issuer
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the issuer
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The domain of the issuer
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }
    
    /// <summary>
    /// The avatar URL of the issuer
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    
    /// <summary>
    /// Indicates if the issuer is on the shortlist
    /// </summary>
    [JsonPropertyName("shortlist")]
    public int Shortlist { get; set; }
    
    /// <summary>
    /// The currencies issued by this issuer
    /// </summary>
    [JsonPropertyName("currencies")]
    public Dictionary<string, CuratedAssetsDetailsCurrency> Currencies { get; set; } = default!;
}

/// <summary>
/// Curated assets currency details
/// </summary>
public class CuratedAssetsDetailsCurrency
{
    /// <summary>
    /// The ID of the currency
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// The ID of the issuer
    /// </summary>
    [JsonPropertyName("issuer_id")]
    public int IssuerId { get; set; }
    
    /// <summary>
    /// The account address of the issuer
    /// </summary>
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; } = default!;
    
    /// <summary>
    /// The currency code
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;
    
    /// <summary>
    /// The formatted currency code
    /// </summary>
    public string CurrencyFormatted => FormatCurrency(Currency);
    
    /// <summary>
    /// The name of the currency
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The avatar URL of the currency
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    
    /// <summary>
    /// Indicates if the currency is on the shortlist
    /// </summary>
    [JsonPropertyName("shortlist")]
    public int Shortlist { get; set; }
    
    private string FormatCurrency(string currency)
    {
        // Implementation will be in an extension method
        return currency;
    }
}

#endregion

#region Rails and Hook Models

/// <summary>
/// Network rails response
/// </summary>
public class RailsResponse
{
    /// <summary>
    /// Creates a new instance of the <see cref="RailsResponse"/> class
    /// </summary>
    /// <param name="networkKey">The network key</param>
    /// <param name="network">The network information</param>
    public RailsResponse(string networkKey, RailsNetwork network)
    {
        NetworkKey = networkKey;
        Network = network;
    }
    
    /// <summary>
    /// The network key
    /// </summary>
    public string NetworkKey { get; }
    
    /// <summary>
    /// The network information
    /// </summary>
    public RailsNetwork Network { get; }
}

/// <summary>
/// Network information
/// </summary>
public class RailsNetwork
{
    /// <summary>
    /// The chain ID
    /// </summary>
    [JsonPropertyName("chain_id")]
    public int ChainId { get; set; }
    
    /// <summary>
    /// The network color
    /// </summary>
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;
    
    /// <summary>
    /// The network name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Indicates if this is a live network
    /// </summary>
    [JsonPropertyName("is_livenet")]
    public bool IsLivenet { get; set; }
    
    /// <summary>
    /// The native asset of the network
    /// </summary>
    [JsonPropertyName("native_asset")]
    public string NativeAsset { get; set; } = default!;
    
    /// <summary>
    /// The network endpoints
    /// </summary>
    [JsonPropertyName("endpoints")]
    public List<RailsNetworkEndpoint> Endpoints { get; set; } = default!;
    
    /// <summary>
    /// The network explorers
    /// </summary>
    [JsonPropertyName("explorers")]
    public List<RailsNetworkExplorer> Explorers { get; set; } = default!;
    
    /// <summary>
    /// The RPC endpoint
    /// </summary>
    [JsonPropertyName("rpc")]
    public string? Rpc { get; set; }
    
    /// <summary>
    /// The definitions URL
    /// </summary>
    [JsonPropertyName("definitions")]
    public string? Definitions { get; set; }
    
    /// <summary>
    /// The network icons
    /// </summary>
    [JsonPropertyName("icons")]
    public RailsNetworkIcons Icons { get; set; } = default!;
}

/// <summary>
/// Network endpoint information
/// </summary>
public class RailsNetworkEndpoint
{
    /// <summary>
    /// The endpoint name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The endpoint URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;
}

/// <summary>
/// Network explorer information
/// </summary>
public class RailsNetworkExplorer
{
    /// <summary>
    /// The explorer name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The transaction URL pattern
    /// </summary>
    [JsonPropertyName("url_tx")]
    public string UrlTx { get; set; } = default!;
    
    /// <summary>
    /// The account URL pattern
    /// </summary>
    [JsonPropertyName("url_account")]
    public string? UrlAccount { get; set; }
    
    /// <summary>
    /// The CTID URL pattern
    /// </summary>
    [JsonPropertyName("url_ctid")]
    public string? UrlCtid { get; set; }
}

/// <summary>
/// Network icons
/// </summary>
public class RailsNetworkIcons
{
    /// <summary>
    /// The square icon URL
    /// </summary>
    [JsonPropertyName("icon_square")]
    public string IconSquare { get; set; } = default!;
    
    /// <summary>
    /// The asset icon URL
    /// </summary>
    [JsonPropertyName("icon_asset")]
    public string IconAsset { get; set; } = default!;
}

/// <summary>
/// Hook information response
/// </summary>
public class HookInfoResponse
{
    /// <summary>
    /// Creates a new instance of the <see cref="HookInfoResponse"/> class
    /// </summary>
    /// <param name="hookHash">The hook hash</param>
    /// <param name="hookInfo">The hook information</param>
    public HookInfoResponse(string hookHash, HookInfo hookInfo)
    {
        HookHash = hookHash;
        HookInfo = hookInfo;
    }
    
    /// <summary>
    /// The hook hash
    /// </summary>
    public string HookHash { get; }
    
    /// <summary>
    /// The hook information
    /// </summary>
    public HookInfo HookInfo { get; }
}

/// <summary>
/// Hook information
/// </summary>
public class HookInfo
{
    /// <summary>
    /// The name of the hook
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// The description of the hook
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
    
    /// <summary>
    /// The creator of the hook
    /// </summary>
    [JsonPropertyName("creator")]
    public HookInfoCreator? Creator { get; set; }
    
    /// <summary>
    /// The xApp associated with the hook
    /// </summary>
    [JsonPropertyName("xapp")]
    public string? Xapp { get; set; }
    
    /// <summary>
    /// The UUID of the application
    /// </summary>
    [JsonPropertyName("appuuid")]
    public string? AppUuid { get; set; }
    
    /// <summary>
    /// The icon URL
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
    
    /// <summary>
    /// The verified accounts
    /// </summary>
    [JsonPropertyName("verifiedAccounts")]
    public List<string>? VerifiedAccounts { get; set; }
    
    /// <summary>
    /// The audit URLs
    /// </summary>
    [JsonPropertyName("audits")]
    public List<string>? Audits { get; set; }
}

/// <summary>
/// Hook creator information
/// </summary>
public class HookInfoCreator
{
    /// <summary>
    /// The name of the creator
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// The email of the creator
    /// </summary>
    [JsonPropertyName("mail")]
    public string? Mail { get; set; }
    
    /// <summary>
    /// The website of the creator
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }
}

#endregion

#region Account Meta Models

/// <summary>
/// Account metadata response
/// </summary>
public class AccountMetaResponse
{
    /// <summary>
    /// The account address
    /// </summary>
    [JsonPropertyName("account")]
    public string Account { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the account has completed KYC
    /// </summary>
    [JsonPropertyName("kycApproved")]
    public bool KycApproved { get; set; }
    
    /// <summary>
    /// Indicates if the account has a Xaman Pro subscription
    /// </summary>
    [JsonPropertyName("xummPro")]
    public bool XummPro { get; set; }
    
    /// <summary>
    /// The avatar URL
    /// </summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = default!;
    
    /// <summary>
    /// The Xaman profile information
    /// </summary>
    [JsonPropertyName("xummProfile")]
    public XamanProfile XummProfile { get; set; } = default!;
    
    /// <summary>
    /// Third-party profile information
    /// </summary>
    [JsonPropertyName("thirdPartyProfiles")]
    public List<XamanThirdPartyProfile> ThirdPartyProfiles { get; set; } = new();
    
    /// <summary>
    /// GlobaliD information
    /// </summary>
    [JsonPropertyName("globalid")]
    public XamanGlobaliD GlobaliD { get; set; } = default!;
}

/// <summary>
/// Xaman profile information
/// </summary>
public class XamanProfile
{
    /// <summary>
    /// The account alias
    /// </summary>
    [JsonPropertyName("accountAlias")]
    public string? AccountAlias { get; set; }
    
    /// <summary>
    /// The owner alias
    /// </summary>
    [JsonPropertyName("ownerAlias")]
    public string? OwnerAlias { get; set; }
}

/// <summary>
/// Third-party profile information
/// </summary>
public class XamanThirdPartyProfile
{
    /// <summary>
    /// The account alias
    /// </summary>
    [JsonPropertyName("accountAlias")]
    public string AccountAlias { get; set; } = default!;
    
    /// <summary>
    /// The source of the profile information
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = default!;
}

/// <summary>
/// GlobaliD information
/// </summary>
public class XamanGlobaliD
{
    /// <summary>
    /// When the account was linked
    /// </summary>
    [JsonPropertyName("linked")]
    public DateTime? Linked { get; set; }
    
    /// <summary>
    /// The profile URL
    /// </summary>
    [JsonPropertyName("profileUrl")]
    public string? ProfileUrl { get; set; }
}

#endregion