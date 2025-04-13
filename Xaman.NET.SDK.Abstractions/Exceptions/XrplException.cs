using System;

namespace Xaman.NET.SDK.Abstractions.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an error communicating with the XRPL
    /// </summary>
    public class XrplException : XamanException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XrplException"/> class
        /// </summary>
        public XrplException() { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XrplException"/> class with a message
        /// </summary>
        /// <param name="message">The exception message</param>
        public XrplException(string message) : base(message) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XrplException"/> class with a message and inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public XrplException(string message, Exception innerException) : base(message, innerException) { }
        
        /// <summary>
        /// Exception thrown when a transaction is not found on the XRPL
        /// </summary>
        public class XrplTransactionNotFoundException : XrplException
        {
            /// <summary>
            /// The transaction hash that was not found
            /// </summary>
            public string TransactionHash { get; }
    
            /// <summary>
            /// Initializes a new instance of the <see cref="XrplException.XrplTransactionNotFoundException"/> class
            /// </summary>
            /// <param name="message">The exception message</param>
            /// <param name="transactionHash">The transaction hash that was not found</param>
            public XrplTransactionNotFoundException(string message, string transactionHash) 
                : base(message)
            {
                TransactionHash = transactionHash;
            }
    
            /// <summary>
            /// Initializes a new instance of the <see cref="XrplException.XrplTransactionNotFoundException"/> class
            /// </summary>
            /// <param name="message">The exception message</param>
            /// <param name="transactionHash">The transaction hash that was not found</param>
            /// <param name="innerException">The inner exception</param>
            public XrplTransactionNotFoundException(string message, string transactionHash, Exception innerException) 
                : base(message, innerException)
            {
                TransactionHash = transactionHash;
            }
        }
    }
}