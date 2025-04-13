using System.Threading;
using System.Threading.Tasks;
using Xaman.NET.SDK.Abstractions.Models.Xrpl;

namespace Xaman.NET.SDK.Abstractions.Clients
{
    /// <summary>
    /// Interface for interacting directly with the XRP Ledger
    /// </summary>
    public interface IXrplClient
    {
        /// <summary>
        /// Gets transaction details from the XRP Ledger
        /// </summary>
        /// <param name="txHash">The transaction hash</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The transaction details</returns>
        Task<XrplTransactionResult> GetTransactionAsync(string txHash, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Checks if a transaction is validated on the XRP Ledger
        /// </summary>
        /// <param name="txHash">The transaction hash</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the transaction is validated, false otherwise</returns>
        Task<bool> IsTransactionValidatedAsync(string txHash, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Gets account information from the XRP Ledger
        /// </summary>
        /// <param name="account">The account address</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<XrplAccountResult> GetAccountInfoAsync(string account, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Polls for a transaction until it is found and validated or the maximum number of attempts is reached
        /// </summary>
        /// <param name="txHash">The transaction hash to poll for</param>
        /// <param name="maxAttempts">Maximum number of polling attempts</param>
        /// <param name="intervalSeconds">Interval between polling attempts in seconds</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The transaction result, or null if the transaction was not found or validated within the maximum attempts</returns>
        Task<XrplTransactionResult?> PollForTransactionAsync(
            string txHash, 
            int maxAttempts = 10, 
            int intervalSeconds = 3,
            CancellationToken cancellationToken = default);
    }
}