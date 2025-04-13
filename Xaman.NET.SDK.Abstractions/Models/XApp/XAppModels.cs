using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Abstractions.Models.XApp;

/// <summary>
/// Response from the one-time token endpoint
/// </summary>
public class XAppOttResponse
{
    /// <summary>
    /// The locale of the user
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }
    
    /// <summary>
    /// The version of the Xaman app
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; }
    
    /// <summary>
    /// The account address
    /// </summary>
    [JsonPropertyName("account")]
    public string? Account { get; set; }
    
    /// <summary>
    /// The account access level
    /// </summary>
    [JsonPropertyName("accountaccess")]
    public string? AccountAccess { get; set; }
    
    /// <summary>
    /// The account type
    /// </summary>
    [JsonPropertyName("accounttype")]
    public string? AccountType { get; set; }
    
    /// <summary>
    /// The style theme
    /// </summary>
    [JsonPropertyName("style")]
    public string? Style { get; set; }
    
    /// <summary>
    /// The origin information
    /// </summary>
    [JsonPropertyName("origin")]
    public XAppOriginResponse? Origin { get; set; }
    
    /// <summary>
    /// The user token
    /// </summary>
    [JsonPropertyName("user")]
    public string User { get; set; } = default!;
    
    /// <summary>
    /// The user device information
    /// </summary>
    [JsonPropertyName("user_device")]
    public XAppUserDeviceResponse? UserDevice { get; set; }
    
    /// <summary>
    /// The account information
    /// </summary>
    [JsonPropertyName("account_info")]
    public XAppAccountInfoResponse AccountInfo { get; set; } = default!;
    
    /// <summary>
    /// The node type
    /// </summary>
    [JsonPropertyName("nodetype")]
    public string? NodeType { get; set; }
    
    /// <summary>
    /// The WebSocket URL for the node
    /// </summary>
    [JsonPropertyName("nodewss")]
    public string? NodeWebSocketSecure { get; set; }
    
    /// <summary>
    /// The network ID
    /// </summary>
    [JsonPropertyName("networkid")]
    public uint? NetworkId { get; set; }
    
    /// <summary>
    /// The currency
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
    
    /// <summary>
    /// The user's subscriptions
    /// </summary>
    [JsonPropertyName("subscriptions")]
    public string[]? Subscriptions { get; set; }
}

/// <summary>
/// Origin information for xApp OTT
/// </summary>
public class XAppOriginResponse
{
    /// <summary>
    /// The type of origin
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    /// <summary>
    /// The data for the origin
    /// </summary>
    [JsonPropertyName("data")]
    public XAppOriginDataResponse? Data { get; set; }
}

/// <summary>
/// Origin data for xApp OTT
/// </summary>
public class XAppOriginDataResponse
{
    /// <summary>
    /// The payload UUID if opened from a payload
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }
}

/// <summary>
/// User device information for xApp OTT
/// </summary>
public class XAppUserDeviceResponse
{
    /// <summary>
    /// The device's currency
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}

/// <summary>
/// Account information for xApp OTT
/// </summary>
public class XAppAccountInfoResponse
{
    /// <summary>
    /// The account address
    /// </summary>
    [JsonPropertyName("account")]
    public string Account { get; set; } = default!;
    
    /// <summary>
    /// The account name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// The account domain
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; }
    
    /// <summary>
    /// Indicates if the account is blocked
    /// </summary>
    [JsonPropertyName("blocked")]
    public bool Blocked { get; set; }
    
    /// <summary>
    /// The source of the account information
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the account has completed KYC
    /// </summary>
    [JsonPropertyName("kycApproved")]
    public bool KycApproved { get; set; }
    
    /// <summary>
    /// Indicates if the account has a pro subscription
    /// </summary>
    [JsonPropertyName("proSubscription")]
    public bool ProSubscription { get; set; }
}

/// <summary>
/// Request to send an event to a user
/// </summary>
public class XAppEventRequest : XAppPushRequest
{
    /// <summary>
    /// Indicates if the event should be silent (no push notification)
    /// </summary>
    [JsonPropertyName("silent")]
    public bool Silent { get; set; }
}

/// <summary>
/// Response from sending an event
/// </summary>
public class XAppEventResponse
{
    /// <summary>
    /// Indicates if the event was pushed successfully
    /// </summary>
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }
    
    /// <summary>
    /// The UUID of the event
    /// </summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }
}

/// <summary>
/// Request to send a push notification
/// </summary>
public class XAppPushRequest
{
    /// <summary>
    /// The user token to send the push to
    /// </summary>
    [JsonPropertyName("user_token")]
    public string UserToken { get; set; } = default!;
    
    /// <summary>
    /// The subtitle for the push notification
    /// </summary>
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }
    
    /// <summary>
    /// The body of the push notification
    /// </summary>
    [JsonPropertyName("body")]
    public string Body { get; set; } = default!;
    
    /// <summary>
    /// Custom data to include with the push
    /// </summary>
    [JsonPropertyName("data")]
    public JsonDocument? Data { get; set; }
}

/// <summary>
/// Response from sending a push notification
/// </summary>
public class XAppPushResponse
{
    /// <summary>
    /// Indicates if the push was sent successfully
    /// </summary>
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }
}