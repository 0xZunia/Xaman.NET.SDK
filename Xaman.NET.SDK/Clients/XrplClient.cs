using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models;
using Xaman.NET.SDK.Abstractions.Models.Xrpl;
using Xaman.NET.SDK.Helpers;

namespace Xaman.NET.SDK.Clients
{
    /// <summary>
    /// Client for interacting with the XRP Ledger
    /// </summary>
    public class XrplClient : IXrplClient
    {
        private readonly ILogger<XrplClient> _logger;
        private readonly XrplOptions _options;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XrplClient"/> class
        /// </summary>
        /// <param name="options">The XRPL options</param>
        /// <param name="logger">The logger</param>
        public XrplClient(IOptions<XrplOptions> options, ILogger<XrplClient> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<XrplTransactionResult> GetTransactionAsync(string txHash, CancellationToken cancellationToken = default)
        {
            var command = new
            {
                id = Guid.NewGuid().ToString(),
                command = "tx",
                transaction = txHash,
                binary = false
            };

            var response = await SendXrplCommandAsync(command, cancellationToken);
    
            if (response.RootElement.TryGetProperty("result", out var result))
            {
                // Vérifier si nous avons une erreur "txnNotFound"
                if (result.TryGetProperty("error", out var error) && 
                    error.GetString() == "txnNotFound")
                {
                    throw new XrplException.XrplTransactionNotFoundException($"Transaction {txHash} not found", txHash);
                }
        
                var transaction = JsonSerializer.Deserialize<XrplTransactionResult>(result.GetRawText());
        
                if (transaction != null)
                {
                    transaction.RawResponse = response;
                    return transaction;
                }
            }
    
            throw new XrplException("Failed to parse transaction response");
        }

        /// <inheritdoc />
        public async Task<bool> IsTransactionValidatedAsync(string txHash, CancellationToken cancellationToken = default)
        {
            try
            {
                var transaction = await GetTransactionAsync(txHash, cancellationToken);
                return transaction.Validated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<XrplAccountResult> GetAccountInfoAsync(string account, CancellationToken cancellationToken = default)
        {
            var command = new
            {
                id = Guid.NewGuid().ToString(),
                command = "account_info",
                account = account,
                strict = true,
                ledger_index = "validated"
            };

            var response = await SendXrplCommandAsync(command, cancellationToken);
            
            if (response.RootElement.TryGetProperty("result", out var result))
            {
                return JsonSerializer.Deserialize<XrplAccountResult>(result.GetRawText()) 
                    ?? throw new XrplException("Failed to parse account info response");
            }
            
            throw new XrplException("Failed to get account information");
        }

        private async Task<JsonDocument> SendXrplCommandAsync(object command, CancellationToken cancellationToken)
        {
            using var client = new ClientWebSocket();
            var uri = new Uri(_options.NodeWebSocketUrl);
            
            try
            {
                await client.ConnectAsync(uri, cancellationToken);
                _logger.LogDebug("Connected to XRPL node at {Url}", uri);
                
                var commandJson = JsonSerializer.Serialize(command);
                var sendBuffer = Encoding.UTF8.GetBytes(commandJson);
                
                await client.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, cancellationToken);
                _logger.LogDebug("Sent command to XRPL node: {Command}", commandJson);
                
                using var ms = new MemoryStream();
                var receiveBuffer = new byte[4096];
                WebSocketReceiveResult result;
                
                do
                {
                    result = await client.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);
                    await ms.WriteAsync(receiveBuffer, 0, result.Count, cancellationToken);
                } 
                while (!result.EndOfMessage);
                
                ms.Seek(0, SeekOrigin.Begin);
                var responseJson = Encoding.UTF8.GetString(ms.ToArray());
                
                _logger.LogDebug("Received response from XRPL node: {Response}", responseJson);
                
                var response = JsonSerializer.Deserialize<JsonDocument>(responseJson);
                
                if (response != null && response.RootElement.TryGetProperty("error", out var error))
                {
                    var message = error.ToString();
                    _logger.LogError("XRPL error: {Error}", message);
                    throw new XrplException($"XRPL error: {message}");
                }
                
                return response ?? throw new XrplException("Empty response from XRPL node");
            }
            catch (Exception ex) when (ex is not XrplException)
            {
                _logger.LogError(ex, "Error communicating with XRPL node");
                throw new XrplException("Error communicating with XRPL node", ex);
            }
            finally
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
            }
        }
        
        /// <inheritdoc />
        public async Task<XrplTransactionResult?> PollForTransactionAsync(
            string txHash, 
            int maxAttempts = 10, 
            int intervalSeconds = 3,
            CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < maxAttempts; i++)
            {
                try
                {
                    _logger.LogInformation("Polling for transaction {TxHash} (attempt {Attempt}/{MaxAttempts})", 
                        txHash, i + 1, maxAttempts);
                
                    var tx = await GetTransactionAsync(txHash, cancellationToken);
            
                    if (tx.Validated)
                    {
                        _logger.LogInformation("Transaction {TxHash} found and validated", txHash);
                        return tx;
                    }
            
                    _logger.LogInformation("Transaction {TxHash} found but not yet validated", txHash);
                }
                catch (XrplException ex) when (ex.Message.Contains("txnNotFound"))
                {
                    _logger.LogInformation("Transaction {TxHash} not found yet, waiting {Interval} seconds before next attempt", 
                        txHash, intervalSeconds);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error polling for transaction {TxHash}", txHash);
                    throw;
                }
        
                if (i < maxAttempts - 1)
                {
                    await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), cancellationToken);
                }
            }
    
            _logger.LogWarning("Transaction {TxHash} not found or validated after {MaxAttempts} attempts", 
                txHash, maxAttempts);
            return null;
        }
    }
}