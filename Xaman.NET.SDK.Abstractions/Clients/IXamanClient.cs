using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.Misc;

namespace Xaman.NET.SDK.Abstractions.Clients;

/// <summary>
/// Main interface for the Xaman SDK client
/// </summary>
public interface IXamanClient
{
    /// <summary>
    /// Gets the payload client for creating and managing sign requests
    /// </summary>
    IXamanPayloadClient Payload { get; }
    
    /// <summary>
    /// Gets the misc client for various utility endpoints
    /// </summary>
    IXamanMiscClient Misc { get; }
    
    /// <summary>
    /// Gets the xApp client for xApp-related functionality
    /// </summary>
    IXamanXAppClient XApp { get; }
    
    /// <summary>
    /// Gets the user store client for managing user data
    /// </summary>
    IXamanUserStoreClient UserStore { get; }
    
    /// <summary>
    /// Gets the XRPL client for direct XRPL interactions
    /// </summary>
    IXrplClient Xrpl { get; }
    
    /// <summary>
    /// Verifies the API credentials and returns basic information about the application
    /// </summary>
    Task<PingResponse> PingAsync(CancellationToken cancellationToken = default);
}