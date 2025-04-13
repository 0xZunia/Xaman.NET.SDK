using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Exceptions;
using Xaman.NET.SDK.Abstractions.Models;
using Xaman.NET.SDK.Abstractions.Models.Errors;
using Xaman.NET.SDK.Helpers;

namespace Xaman.NET.SDK.Core.Http;

/// <summary>
/// HTTP client for communicating with the Xaman API
/// </summary>
public class XamanHttpClient : IXamanHttpClient
{
    private static readonly string? UserAgentVersion = typeof(XamanHttpClient).Assembly.GetName().Version?.ToString();
    private readonly XamanOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<XamanHttpClient> _logger;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="XamanHttpClient"/> class
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory</param>
    /// <param name="options">The Xaman options</param>
    /// <param name="logger">The logger</param>
    public XamanHttpClient(
        IHttpClientFactory httpClientFactory,
        IOptions<XamanOptions> options,
        ILogger<XamanHttpClient> logger)
    {
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(HttpMethod.Get, endpoint, true, default, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> GetAsync<T>(HttpClient client, string endpoint, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(client, HttpMethod.Get, endpoint, default, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> GetPublicAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(HttpMethod.Get, endpoint, false, default, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> PostAsync<T>(string endpoint, object content, CancellationToken cancellationToken = default)
    {
        return await PostAsync<T>(endpoint, JsonHelper.Serialize(content), cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> PostAsync<T>(string endpoint, string json, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(HttpMethod.Post, endpoint, true, json, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> PostAsync<T>(HttpClient client, string endpoint, string json, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(client, HttpMethod.Post, endpoint, json, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(HttpMethod.Delete, endpoint, true, default, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<T> DeleteAsync<T>(HttpClient client, string endpoint, CancellationToken cancellationToken = default)
    {
        return await SendAsync<T>(client, HttpMethod.Delete, endpoint, default, cancellationToken);
    }
    
    /// <inheritdoc />
    public HttpClient GetHttpClient(bool setCredentials = true)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Clear();
        
        if (setCredentials)
        {
            httpClient.DefaultRequestHeaders.Add("X-API-Key", _options.ApiKey);
            httpClient.DefaultRequestHeaders.Add("X-API-Secret", _options.ApiSecret);
        }
        
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Add("User-Agent", $"XamanDotNet/{UserAgentVersion}");
        httpClient.Timeout = _options.HttpTimeout;
        
        return httpClient;
    }
    
    private Task<T> SendAsync<T>(HttpMethod method, string endpoint, bool setCredentials, string? json, CancellationToken cancellationToken)
    {
        var client = GetHttpClient(setCredentials);
        return SendAsync<T>(client, method, endpoint, json, cancellationToken);
    }
    
    private async Task<T> SendAsync<T>(HttpClient client, HttpMethod method, string endpoint, string? json, CancellationToken cancellationToken)
    {
        try
        {
            var retryCount = 0;
            HttpResponseMessage? response = null;
            
            while (retryCount <= _options.MaxRetries)
            {
                try
                {
                    using var requestMessage = new HttpRequestMessage(method, GetRequestUrl(endpoint));
                    
                    if (json != null)
                    {
                        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    }
                    
                    response = await client.SendAsync(requestMessage, cancellationToken);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        break;
                    }
                    
                    if ((int)response.StatusCode >= 500)
                    {
                        // Only retry on server errors
                        retryCount++;
                        
                        if (retryCount <= _options.MaxRetries)
                        {
                            await Task.Delay(_options.RetryDelay * retryCount, cancellationToken);
                            continue;
                        }
                    }
                    
                    break;
                }
                catch (HttpRequestException) when (retryCount < _options.MaxRetries)
                {
                    retryCount++;
                    await Task.Delay(_options.RetryDelay * retryCount, cancellationToken);
                }
            }
            
            if (response == null)
            {
                throw new XamanApiException(500, "Unable to send request to Xaman API after multiple retries");
            }
            
            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw GetHttpRequestException(response, responseText);
            }
            
            var result = JsonSerializer.Deserialize<T>(responseText, JsonHelper.SerializerOptions);
            if (result == null)
            {
                throw new XamanApiException(500, $"Unexpected response for {endpoint}: Unable to deserialize response");
            }
            
            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogDebug("Request canceled for endpoint: {Endpoint}", endpoint);
            throw;
        }
        catch (Exception ex) when (ex is not XamanException && ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Unexpected error from Xaman API [{Method}:{Endpoint}]", method, endpoint);
            throw new XamanApiException(500, $"Unexpected error from Xaman API: {ex.Message}", ex);
        }
    }
    
    private HttpRequestException GetHttpRequestException(HttpResponseMessage response, string responseText)
    {
        XamanApiException? exception = null;
        
        try
        {
            var fatalApiError = JsonSerializer.Deserialize<XamanFatalApiError>(responseText, JsonHelper.SerializerOptions);
            if (fatalApiError is { Error: true } && !string.IsNullOrWhiteSpace(fatalApiError.Message))
            {
                exception = new XamanApiException((int)response.StatusCode, fatalApiError.Message, fatalApiError.Reference, fatalApiError.Code);
            }
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "No {Type} available in unsuccessful response body of request: {Url}",
                nameof(XamanFatalApiError), response.RequestMessage?.RequestUri);
        }
        
        try
        {
            if (exception == null)
            {
                var apiError = JsonSerializer.Deserialize<XamanApiError>(responseText, JsonHelper.SerializerOptions);
                if (apiError?.Error != null)
                {
                    var message = $"Error code {apiError.Error.Code}, see Xaman Dev Console, reference: '{apiError.Error.Reference}'.";
                    exception = new XamanApiException((int)response.StatusCode, message, apiError.Error.Reference, apiError.Error.Code);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "No {Type} available in unsuccessful response body of request: {Url}",
                nameof(XamanApiError), response.RequestMessage?.RequestUri);
        }
        
        exception ??= new XamanApiException((int)response.StatusCode, response.ReasonPhrase ?? "Unknown error");
        
        // Convert XamanApiException to HttpRequestException
        return new HttpRequestException(exception.Message, exception, response.StatusCode);
    }
    
    private string GetRequestUrl(string endpoint)
    {
        var result = _options.RestClientAddress;
        if (!result.EndsWith('/'))
        {
            result += "/";
        }
        
        result += endpoint;
        return result;
    }
}