using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models;
using Xaman.NET.SDK.Clients;

namespace Xaman.NET.SDK;

/// <summary>
/// Builder class for creating and configuring an instance of <see cref="XamanClient"/>
/// </summary>
public class XamanClientBuilder
{
    private readonly XamanOptions _options = new();
    private readonly ServiceCollection _services = new();
    private ILoggerFactory? _loggerFactory;
    
    /// <summary>
    /// Sets the API key for the Xaman API
    /// </summary>
    /// <param name="apiKey">The API key</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithApiKey(string apiKey)
    {
        _options.ApiKey = apiKey;
        return this;
    }
    
    /// <summary>
    /// Sets the API secret for the Xaman API
    /// </summary>
    /// <param name="apiSecret">The API secret</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithApiSecret(string apiSecret)
    {
        _options.ApiSecret = apiSecret;
        return this;
    }
    
    /// <summary>
    /// Sets the base URL for the Xaman API
    /// </summary>
    /// <param name="baseUrl">The base URL</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithBaseUrl(string baseUrl)
    {
        _options.RestClientAddress = baseUrl;
        return this;
    }
    
    /// <summary>
    /// Sets the HTTP timeout for API requests
    /// </summary>
    /// <param name="timeout">The timeout</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithTimeout(TimeSpan timeout)
    {
        _options.HttpTimeout = timeout;
        return this;
    }
    
    /// <summary>
    /// Sets the maximum number of retries for failed requests
    /// </summary>
    /// <param name="maxRetries">The maximum number of retries</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithMaxRetries(int maxRetries)
    {
        _options.MaxRetries = maxRetries;
        return this;
    }
    
    /// <summary>
    /// Sets the delay between retries for failed requests
    /// </summary>
    /// <param name="retryDelay">The retry delay</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithRetryDelay(TimeSpan retryDelay)
    {
        _options.RetryDelay = retryDelay;
        return this;
    }
    
    /// <summary>
    /// Sets the logger factory for the Xaman SDK
    /// </summary>
    /// <param name="loggerFactory">The logger factory</param>
    /// <returns>The builder instance</returns>
    public XamanClientBuilder WithLoggerFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        return this;
    }
    
    /// <summary>
    /// Builds an instance of <see cref="IXamanClient"/>
    /// </summary>
    /// <returns>The configured client</returns>
    /// <exception cref="XamanConfigurationException">Thrown when required configuration is missing</exception>
    public IXamanClient Build()
    {
        ValidateOptions();
        
        // Configure services
        _services.AddSingleton(Options.Create(_options));
        
        if (_loggerFactory != null)
        {
            _services.AddSingleton(_loggerFactory);
            _services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
        else
        {
            _services.AddLogging(builder => builder.AddConsole());
        }
        
        _services.AddHttpClient();
        _services.AddSingleton<IXamanHttpClient, Core.Http.XamanHttpClient>();
        _services.AddSingleton<IXamanPayloadClient, XamanPayloadClient>();
        _services.AddSingleton<IXamanMiscClient, XamanMiscClient>();
        _services.AddSingleton<IXamanXAppClient, XamanXAppClient>();
        _services.AddSingleton<IXamanUserStoreClient, XamanUserStoreClient>();
        _services.AddSingleton<IXamanWebSocket, WebSocket.XamanWebSocket>();
        _services.AddSingleton<IXrplClient, XrplClient>();
        _services.AddSingleton<IXamanClient, XamanClient>();
        
        // Build service provider
        var serviceProvider = _services.BuildServiceProvider();
        
        // Resolve the client
        return serviceProvider.GetRequiredService<IXamanClient>();
    }
    
    private void ValidateOptions()
    {
        // Validate API key
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            throw new XamanConfigurationException("API key is required. Use WithApiKey() to set it.");
        }
        
        // Validate API secret
        if (string.IsNullOrWhiteSpace(_options.ApiSecret))
        {
            throw new XamanConfigurationException("API secret is required. Use WithApiSecret() to set it.");
        }
        
        _options.ValidateApiKey();
        _options.ValidateApiSecret();
    }
}