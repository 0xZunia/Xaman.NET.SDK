using System;

namespace Xaman.NET.SDK.Abstractions.Models
{
    /// <summary>
    /// Configuration options for the XRPL client
    /// </summary>
    public class XrplOptions
    {
        /// <summary>
        /// The WebSocket URL for the XRPL node
        /// </summary>
        public string NodeWebSocketUrl { get; set; } = "wss://testnet.xrpl-labs.com/";
        
        /// <summary>
        /// The number of retries for XRPL requests
        /// </summary>
        public int MaxRetries { get; set; } = 5;
        
        /// <summary>
        /// The delay between retries for XRPL requests
        /// </summary>
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(3);
    }
}