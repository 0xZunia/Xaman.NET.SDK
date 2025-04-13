using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xaman.NET.SDK.Abstractions.Models.Misc;

namespace Xaman.NET.SDK.Abstractions.Models.Payload;

/// <summary>
/// Base class for payload requests
/// </summary>
public abstract class PayloadRequestBase
{
    /// <summary>
    /// User (Push) token, to deliver a signing request directly to the mobile device of a user
    /// </summary>
    [JsonPropertyName("user_token")]
    public string? UserToken { get; set; }
    
    /// <summary>
    /// Payload options
    /// </summary>
    [JsonPropertyName("options")]
    public PayloadOptions? Options { get; set; }
    
    /// <summary>
    /// Custom metadata for the payload
    /// </summary>
    [JsonPropertyName("custom_meta")]
    public PayloadCustomMeta? CustomMeta { get; set; }
}

/// <summary>
/// JSON payload request
/// </summary>
public class JsonPayloadRequest : PayloadRequestBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPayloadRequest"/> class
    /// </summary>
    /// <param name="txJson">The transaction JSON</param>
    public JsonPayloadRequest(string txJson)
    {
        TxJson = JsonDocument.Parse(txJson);
    }
    
    /// <summary>
    /// Transaction template in JSON format
    /// </summary>
    [JsonPropertyName("txjson")]
    public JsonDocument TxJson { get; }
}

/// <summary>
/// Blob payload request
/// </summary>
public class BlobPayloadRequest : PayloadRequestBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlobPayloadRequest"/> class
    /// </summary>
    /// <param name="txBlob">The transaction blob</param>
    public BlobPayloadRequest(string txBlob)
    {
        TxBlob = txBlob;
    }
    
    /// <summary>
    /// Transaction template in hexadecimal blob format
    /// </summary>
    [JsonPropertyName("txblob")]
    public string TxBlob { get; }
}

/// <summary>
/// Transaction payload
/// </summary>
public class TransactionPayload : Dictionary<string, object>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionPayload"/> class
    /// </summary>
    /// <param name="transactionType">The transaction type</param>
    public TransactionPayload(string transactionType)
    {
        Add("TransactionType", transactionType);
    }
}

/// <summary>
/// Payload options
/// </summary>
public class PayloadOptions
{
    /// <summary>
    /// Should the Xaman app submit to the XRPL after signing?
    /// </summary>
    [JsonPropertyName("submit")]
    public bool? Submit { get; set; }
    
    /// <summary>
    /// Allow pathfinding for regular Payment type transactions
    /// </summary>
    [JsonPropertyName("pathfinding")]
    public bool? Pathfinding { get; set; }
    
    /// <summary>
    /// Allow Xaman clients < version 2.4.0 to fall back from modern pathfinding UX to a native 1:1 asset payment
    /// </summary>
    [JsonPropertyName("pathfinding_fallback")]
    public bool? PathfindingFallback { get; set; }
    
    /// <summary>
    /// Should the transaction be signed as a multi sign transaction?
    /// </summary>
    [JsonPropertyName("multisign")]
    public bool? MultiSign { get; set; }
    
    /// <summary>
    /// After how many minutes should the payload expire?
    /// </summary>
    [JsonPropertyName("expire")]
    public int? Expire { get; set; }
    
    /// <summary>
    /// Force any of the provided accounts to sign
    /// </summary>
    [JsonPropertyName("signers")]
    public string[]? Signers { get; set; }
    
    /// <summary>
    /// Force the payload to be opened on a specific network
    /// </summary>
    [JsonPropertyName("force_network")]
    public string? ForceNetwork { get; set; }
    
    /// <summary>
    /// Where should the user be redirected to after resolving the payload?
    /// </summary>
    [JsonPropertyName("return_url")]
    public PayloadReturnUrl? ReturnUrl { get; set; }
}

/// <summary>
/// Payload return URL
/// </summary>
public class PayloadReturnUrl
{
    /// <summary>
    /// Smart device application return URL (Optional)
    /// </summary>
    [JsonPropertyName("app")]
    public string? App { get; set; }
    
    /// <summary>
    /// Web (browser) return URL (Optional)
    /// </summary>
    [JsonPropertyName("web")]
    public string? Web { get; set; }
}

/// <summary>
/// Payload custom metadata
/// </summary>
public class PayloadCustomMeta
{
    /// <summary>
    /// Your own identifier for this payload. This identifier must be unique.
    /// </summary>
    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }
    
    /// <summary>
    /// A custom JSON object containing metadata, attached to this specific payload
    /// </summary>
    [JsonPropertyName("blob")]
    public JsonDocument? Blob { get; set; }
    
    /// <summary>
    /// A message (instruction, reason for signing) to display to the Xaman user
    /// </summary>
    [JsonPropertyName("instruction")]
    public string? Instruction { get; set; }
}

/// <summary>
/// Response from creating a payload
/// </summary>
public class PayloadResponse
{
    /// <summary>
    /// The UUID of the created payload
    /// </summary>
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; } = default!;
    
    /// <summary>
    /// URLs to handle the payload
    /// </summary>
    [JsonPropertyName("next")]
    public PayloadNextResponse Next { get; set; } = default!;
    
    /// <summary>
    /// References to payload resources
    /// </summary>
    [JsonPropertyName("refs")]
    public PayloadRefsResponse Refs { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the payload was pushed to the user's device
    /// </summary>
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }
}

/// <summary>
/// Next URLs for the payload
/// </summary>
public class PayloadNextResponse
{
    /// <summary>
    /// URL to open the payload in Xaman
    /// </summary>
    [JsonPropertyName("always")]
    public string Always { get; set; } = default!;
    
    /// <summary>
    /// URL to open the payload via QR code if no push notification was received
    /// </summary>
    [JsonPropertyName("no_push_msg_received")]
    public string? NoPushMessageReceived { get; set; }
}

/// <summary>
/// References to payload resources
/// </summary>
public class PayloadRefsResponse
{
    /// <summary>
    /// URL to the QR code PNG image
    /// </summary>
    [JsonPropertyName("qr_png")]
    public string QrPng { get; set; } = default!;
    
    /// <summary>
    /// URL to the QR code matrix data
    /// </summary>
    [JsonPropertyName("qr_matrix")]
    public string QrMatrix { get; set; } = default!;
    
    /// <summary>
    /// QR code quality options
    /// </summary>
    [JsonPropertyName("qr_uri_quality_opts")]
    public List<string> QrUriQualityOpts { get; set; } = default!;
    
    /// <summary>
    /// URL to the WebSocket for real-time status updates
    /// </summary>
    [JsonPropertyName("websocket_status")]
    public string WebsocketStatus { get; set; } = default!;
}

/// <summary>
/// Detailed payload information
/// </summary>
public class PayloadDetails
{
    /// <summary>
    /// Metadata about the payload
    /// </summary>
    [JsonPropertyName("meta")]
    public PayloadDetailsMeta Meta { get; set; } = default!;
    
    /// <summary>
    /// Application information
    /// </summary>
    [JsonPropertyName("application")]
    public XamanApplication Application { get; set; } = default!;
    
    /// <summary>
    /// Payload content
    /// </summary>
    [JsonPropertyName("payload")]
    public PayloadDetailsResponse Payload { get; set; } = default!;
    
    /// <summary>
    /// Response information after the payload is resolved
    /// </summary>
    [JsonPropertyName("response")]
    public PayloadResponse Response { get; set; } = default!;
    
    /// <summary>
    /// Custom metadata
    /// </summary>
    [JsonPropertyName("custom_meta")]
    public PayloadCustomMeta? CustomMeta { get; set; }
}

/// <summary>
/// Payload metadata
/// </summary>
public class PayloadDetailsMeta
{
    /// <summary>
    /// Indicates if the payload exists
    /// </summary>
    [JsonPropertyName("exists")]
    public bool Exists { get; set; }
    
    /// <summary>
    /// The UUID of the payload
    /// </summary>
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the payload is for a multi-signature transaction
    /// </summary>
    [JsonPropertyName("multisign")]
    public bool Multisign { get; set; }
    
    /// <summary>
    /// Indicates if the payload should be submitted to the XRPL
    /// </summary>
    [JsonPropertyName("submit")]
    public bool Submit { get; set; }
    
    /// <summary>
    /// Indicates if pathfinding is enabled
    /// </summary>
    [JsonPropertyName("pathfinding")]
    public bool Pathfinding { get; set; }
    
    /// <summary>
    /// Indicates if pathfinding fallback is enabled
    /// </summary>
    [JsonPropertyName("pathfinding_fallback")]
    public bool PathfindingFallback { get; set; }
    
    /// <summary>
    /// The forced network, if any
    /// </summary>
    [JsonPropertyName("force_network")]
    public string? ForceNetwork { get; set; }
    
    /// <summary>
    /// The destination address
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; } = default!;
    
    /// <summary>
    /// The resolved destination address
    /// </summary>
    [JsonPropertyName("resolved_destination")]
    public string ResolvedDestination { get; set; } = default!;
    
    /// <summary>
    /// Indicates if the payload has been resolved
    /// </summary>
    [JsonPropertyName("resolved")]
    public bool Resolved { get; set; }
    
    /// <summary>
    /// Indicates if the payload has been signed
    /// </summary>
    [JsonPropertyName("signed")]
    public bool Signed { get; set; }
    
    /// <summary>
    /// Indicates if the payload has been cancelled
    /// </summary>
    [JsonPropertyName("cancelled")]
    public bool Cancelled { get; set; }
    
    /// <summary>
    /// Indicates if the payload has expired
    /// </summary>
    [JsonPropertyName("expired")]
    public bool Expired { get; set; }
    
    /// <summary>
    /// Indicates if the payload was pushed to the user's device
    /// </summary>
    [JsonPropertyName("pushed")]
    public bool Pushed { get; set; }
    
    /// <summary>
    /// Indicates if the app was opened to view the payload
    /// </summary>
    [JsonPropertyName("app_opened")]
    public bool AppOpened { get; set; }
    
    /// <summary>
    /// Indicates if the app was opened via a deeplink
    /// </summary>
    [JsonPropertyName("opened_by_deeplink")]
    public bool? OpenedByDeeplink { get; set; }
    
    /// <summary>
    /// Indicates if the payload is immutable
    /// </summary>
    [JsonPropertyName("immutable")]
    public bool? Immutable { get; set; }
    
    /// <summary>
    /// The return URL for the app
    /// </summary>
    [JsonPropertyName("return_url_app")]
    public string? ReturnUrlApp { get; set; }
    
    /// <summary>
    /// The return URL for the web
    /// </summary>
    [JsonPropertyName("return_url_web")]
    public string? ReturnUrlWeb { get; set; }
    
    /// <summary>
    /// Indicates if the payload is for an xApp
    /// </summary>
    [JsonPropertyName("is_xapp")]
    public bool IsXapp { get; set; }
    
    /// <summary>
    /// The accounts that can sign the payload
    /// </summary>
    [JsonPropertyName("signers")]
    public string[]? Signers { get; set; }
}

/// <summary>
/// Payload details response
/// </summary>
public class PayloadDetailsResponse
{
    /// <summary>
    /// The type of transaction
    /// </summary>
    [JsonPropertyName("tx_type")]
    public string TxType { get; set; } = default!;
    
    /// <summary>
    /// The destination address
    /// </summary>
    [JsonPropertyName("tx_destination")]
    public string TxDestination { get; set; } = default!;
    
    /// <summary>
    /// The destination tag
    /// </summary>
    [JsonPropertyName("tx_destination_tag")]
    public uint? TxDestinationTag { get; set; }
    
    /// <summary>
    /// The request JSON
    /// </summary>
    [JsonPropertyName("request_json")]
    public JsonDocument RequestJson { get; set; } = default!;
    
    /// <summary>
    /// The origin type
    /// </summary>
    [JsonPropertyName("origintype")]
    public string? OriginType { get; set; }
    
    /// <summary>
    /// The sign method
    /// </summary>
    [JsonPropertyName("signmethod")]
    public string? SignMethod { get; set; }
    
    /// <summary>
    /// When the payload was created
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// When the payload expires
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }
    
    /// <summary>
    /// How many seconds until the payload expires
    /// </summary>
    [JsonPropertyName("expires_in_seconds")]
    public int ExpiresInSeconds { get; set; }
}

/// <summary>
/// Response when deleting a payload
/// </summary>
public class DeletePayloadResponse
{
    /// <summary>
    /// The result of the delete operation
    /// </summary>
    [JsonPropertyName("result")]
    public DeletePayloadResult Result { get; set; } = default!;
    
    /// <summary>
    /// Metadata about the payload
    /// </summary>
    [JsonPropertyName("meta")]
    public PayloadDetailsMeta Meta { get; set; } = default!;
    
    /// <summary>
    /// Custom metadata
    /// </summary>
    [JsonPropertyName("custom_meta")]
    public PayloadCustomMeta? CustomMeta { get; set; }
}

/// <summary>
/// Result of deleting a payload
/// </summary>
public class DeletePayloadResult
{
    /// <summary>
    /// Indicates if the payload was cancelled
    /// </summary>
    [JsonPropertyName("cancelled")]
    public bool Cancelled { get; set; }
    
    /// <summary>
    /// The reason for the cancellation
    /// </summary>
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;
}