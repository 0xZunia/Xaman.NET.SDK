using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xaman.NET.SDK.Abstractions.Clients;
using Xaman.NET.SDK.Abstractions.Models;
using Xaman.NET.SDK.Clients;
using Xaman.NET.SDK.Core.Http;
using Xaman.NET.SDK.WebSocket;

namespace Xaman.NET.SDK;

/// <summary>
/// Extension methods for setting up Xaman services in an <see cref="IServiceCollection"/>
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddXaman(this IServiceCollection services, Action<XamanOptions> configureOptions)
    {
        // Configure options
        services.Configure(configureOptions);
        
        // Register XRPL services
        services.AddXrpl();
        
        // Register existing services
        services.AddHttpClient();
        services.AddSingleton<IXamanHttpClient, XamanHttpClient>();
        services.AddSingleton<IXamanWebSocket, XamanWebSocket>();
        services.AddSingleton<IXamanPayloadClient, XamanPayloadClient>();
        services.AddSingleton<IXamanMiscClient, XamanMiscClient>();
        services.AddSingleton<IXamanXAppClient, XamanXAppClient>();
        services.AddSingleton<IXamanUserStoreClient, XamanUserStoreClient>();
        services.AddSingleton<IXamanClient, XamanClient>();
        
        return services;
    }
    
    public static IServiceCollection AddXrpl(this IServiceCollection services, Action<XrplOptions>? configureOptions = null)
    {
        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.Configure<XrplOptions>(options => { });
        }
        
        services.AddSingleton<IXrplClient, XrplClient>();
        
        return services;
    }
}