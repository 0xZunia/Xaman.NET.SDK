using System;

namespace Xaman.NET.SDK.Abstractions.Exceptions;

/// <summary>
/// Base exception for all Xaman SDK exceptions
/// </summary>
public class XamanException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanException"/> class
    /// </summary>
    public XamanException() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanException"/> class with a message
    /// </summary>
    /// <param name="message">The exception message</param>
    public XamanException(string message) : base(message) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanException"/> class with a message and inner exception
    /// </summary>
    /// <param name="message">The exception message</param>
    /// <param name="innerException">The inner exception</param>
    public XamanException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when there is an error with the API configuration
/// </summary>
public class XamanConfigurationException : XamanException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanConfigurationException"/> class
    /// </summary>
    public XamanConfigurationException() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanConfigurationException"/> class with a message
    /// </summary>
    /// <param name="message">The exception message</param>
    public XamanConfigurationException(string message) : base(message) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanConfigurationException"/> class with a message and inner exception
    /// </summary>
    /// <param name="message">The exception message</param>
    /// <param name="innerException">The inner exception</param>
    public XamanConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when there is an error with the API request
/// </summary>
public class XamanApiException : XamanException
{
    /// <summary>
    /// The HTTP status code of the error
    /// </summary>
    public int StatusCode { get; }
    
    /// <summary>
    /// The error reference from the API
    /// </summary>
    public string? Reference { get; }
    
    /// <summary>
    /// The error code from the API
    /// </summary>
    public int? Code { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanApiException"/> class
    /// </summary>
    /// <param name="statusCode">The HTTP status code</param>
    /// <param name="message">The exception message</param>
    /// <param name="reference">The error reference</param>
    /// <param name="code">The error code</param>
    public XamanApiException(int statusCode, string message, string? reference = null, int? code = null) 
        : base(message)
    {
        StatusCode = statusCode;
        Reference = reference;
        Code = code;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanApiException"/> class
    /// </summary>
    /// <param name="statusCode">The HTTP status code</param>
    /// <param name="message">The exception message</param>
    /// <param name="innerException">The inner exception</param>
    /// <param name="reference">The error reference</param>
    /// <param name="code">The error code</param>
    public XamanApiException(int statusCode, string message, Exception innerException, string? reference = null, int? code = null) 
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Reference = reference;
        Code = code;
    }
}

/// <summary>
/// Exception thrown when a required parameter is missing
/// </summary>
public class XamanValidationException : XamanException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanValidationException"/> class
    /// </summary>
    public XamanValidationException() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanValidationException"/> class with a message
    /// </summary>
    /// <param name="message">The exception message</param>
    public XamanValidationException(string message) : base(message) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanValidationException"/> class with a message and inner exception
    /// </summary>
    /// <param name="message">The exception message</param>
    /// <param name="innerException">The inner exception</param>
    public XamanValidationException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when a payload is not found
/// </summary>
public class XamanPayloadNotFoundException : XamanException
{
    /// <summary>
    /// The payload UUID that was not found
    /// </summary>
    public string PayloadUuid { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanPayloadNotFoundException"/> class
    /// </summary>
    /// <param name="payloadUuid">The payload UUID</param>
    public XamanPayloadNotFoundException(string payloadUuid) 
        : base($"Payload with UUID '{payloadUuid}' not found")
    {
        PayloadUuid = payloadUuid;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanPayloadNotFoundException"/> class
    /// </summary>
    /// <param name="payloadUuid">The payload UUID</param>
    /// <param name="innerException">The inner exception</param>
    public XamanPayloadNotFoundException(string payloadUuid, Exception innerException) 
        : base($"Payload with UUID '{payloadUuid}' not found", innerException)
    {
        PayloadUuid = payloadUuid;
    }
}

/// <summary>
/// Exception thrown when a WebSocket error occurs
/// </summary>
public class XamanWebSocketException : XamanException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanWebSocketException"/> class
    /// </summary>
    public XamanWebSocketException() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanWebSocketException"/> class with a message
    /// </summary>
    /// <param name="message">The exception message</param>
    public XamanWebSocketException(string message) : base(message) { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanWebSocketException"/> class with a message and inner exception
    /// </summary>
    /// <param name="message">The exception message</param>
    /// <param name="innerException">The inner exception</param>
    public XamanWebSocketException(string message, Exception innerException) : base(message, innerException) { }
}