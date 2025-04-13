# Xaman.NET.SDK

A comprehensive .NET SDK for integrating with the Xaman (formerly XUMM) app and the XRP Ledger.

## Overview

Xaman.NET.SDK is a C# client library that provides a simple and intuitive way to interact with the Xaman API, allowing developers to integrate XRPL signing capabilities into their .NET applications. This SDK simplifies the process of creating and managing payloads, interacting with xApps, and communicating directly with the XRP Ledger.

## Features

- **Complete API Coverage**: Supports all Xaman API endpoints
- **Strongly Typed**: Fully typed C# models for request and response objects
- **Async Support**: Designed with modern async patterns
- **WebSocket Integration**: Real-time payload status updates
- **XRPL Interaction**: Direct communication with the XRP Ledger
- **Dependency Injection**: Built-in support for Microsoft DI
- **Flexible Configuration**: Multiple ways to configure the SDK
- **Comprehensive Error Handling**: Detailed exception types for better error management
- **Extensive Documentation**: XML documentation for all public APIs

## Installation

Install the package from NuGet:

```bash
dotnet add package Xaman.NET.SDK
```

## Quick Start

### Basic Setup

```csharp
// Create a client using the builder pattern
var xamanClient = new XamanClientBuilder()
    .WithApiKey("your-api-key")
    .WithApiSecret("your-api-secret")
    .Build();
```

### Using Dependency Injection

```csharp
// In your Startup.cs or Program.cs
services.AddXaman(options => {
    options.ApiKey = "your-api-key";
    options.ApiSecret = "your-api-secret";
});

// Then inject IXamanClient into your services
public class MyService
{
    private readonly IXamanClient _xamanClient;

    public MyService(IXamanClient xamanClient)
    {
        _xamanClient = xamanClient;
    }
}
```

## Usage Examples

### Creating a Payment Payload

```csharp
// Create a basic payment transaction
var payloadRequest = new JsonPayloadRequest(JsonSerializer.Serialize(new
{
    TransactionType = "Payment",
    Destination = "rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY",
    Amount = "1000000" // 1 XRP in drops
}))
{
    Options = new PayloadOptions
    {
        Submit = true,
        ReturnUrl = new PayloadReturnUrl
        {
            Web = "https://example.com/return?id={id}"
        }
    }
};

// Create the payload
var payloadResponse = await xamanClient.Payload.CreateAsync(payloadRequest);

// Get the QR code and URLs
var qrCodeUrl = payloadResponse.Refs.QrPng;
var xummAppDeeplink = payloadResponse.Next.Always;
```

### Subscribing to Payload Updates

```csharp
// Create event handler
void HandlePayloadEvent(object? sender, PayloadEventArgs e)
{
    var eventData = e.Data.RootElement;
    
    if (eventData.TryGetProperty("signed", out var signed))
    {
        bool wasRejected = !signed.GetBoolean();
        
        if (!wasRejected && eventData.TryGetProperty("txid", out var txid))
        {
            string? txHash = txid.GetString();
            Console.WriteLine($"Transaction signed: {txHash}");
        }
        else
        {
            Console.WriteLine("Transaction rejected");
        }
        
        // Close the connection
        e.CloseConnectionAsync();
    }
}

// Subscribe to updates
await xamanClient.Payload.SubscribeAsync(
    payloadResponse, 
    HandlePayloadEvent, 
    CancellationToken.None
);
```

### Checking Transaction Status

```csharp
// Poll for a transaction until it's validated
var transaction = await xamanClient.Xrpl.PollForTransactionAsync(txHash);

if (transaction != null && transaction.Validated)
{
    Console.WriteLine("Transaction validated on the XRP Ledger!");
    
    // Check the delivered amount
    if (transaction.Meta?.DeliveredAmount != null)
    {
        var (amount, currency) = XrplAmountHelper.ParseAmount(transaction.Meta.DeliveredAmount);
        Console.WriteLine($"Delivered amount: {amount} {currency}");
    }
}
```

### Working with xApps

```csharp
// Authorize JWT for xApp
var authResponse = await xamanClient.XAppJwt.AuthorizeAsync(oneTimeToken);
var jwt = authResponse.JWT;

// Get user data
var userData = await xamanClient.XAppJwt.GetUserDataAsync(jwt, "preferences");

// Store user data
await xamanClient.XAppJwt.SetUserDataAsync(jwt, "preferences", 
    JsonSerializer.Serialize(new { theme = "dark" }));
```

## Advanced Usage

### Transaction Types

The SDK supports all XRPL transaction types, including:

- Payment
- TrustSet
- AccountSet
- EscrowCreate
- EscrowFinish
- OfferCreate
- NFTokenMint
- NFTokenCreateOffer
- And more...

### WebSocket Subscriptions

Real-time updates for payload status using WebSockets:

```csharp
// Create and immediately subscribe to a payload
var payloadResponse = await xamanClient.Payload.CreateAndSubscribeAsync(
    payloadRequest,
    HandlePayloadEvent,
    cancellationToken
);
```

### Direct XRPL Interaction

```csharp
// Get account information
var accountInfo = await xamanClient.Xrpl.GetAccountInfoAsync("rPEPPER7kfTD9w2To4CQk6UCfuHM9c6GDY");
Console.WriteLine($"Account Balance: {decimal.Parse(accountInfo.AccountData.Balance) / 1000000m} XRP");

// Check transaction details
var tx = await xamanClient.Xrpl.GetTransactionAsync(txHash);
Console.WriteLine($"Transaction result: {tx.Meta?.TransactionResult}");
```

## Components

The SDK is organized into several logical components:

- **XamanClient**: Main entry point for the SDK
- **PayloadClient**: Create and manage sign requests
- **MiscClient**: Utility endpoints like exchange rates, KYC status, etc.
- **XAppClient**: xApp integration
- **UserStoreClient**: App storage management
- **XrplClient**: Direct XRPL interaction

## Error Handling

The SDK provides detailed exception types for better error management:

```csharp
try
{
    var payload = await xamanClient.Payload.GetAsync(payloadUuid, true);
    // Process payload
}
catch (XamanPayloadNotFoundException ex)
{
    Console.WriteLine($"Payload not found: {ex.PayloadUuid}");
}
catch (XamanApiException ex)
{
    Console.WriteLine($"API error: {ex.StatusCode} - {ex.Message}");
}
catch (XamanException ex)
{
    Console.WriteLine($"Generic Xaman error: {ex.Message}");
}
```

## Configuration Options

The SDK provides various configuration options:

```csharp
var xamanClient = new XamanClientBuilder()
    .WithApiKey("your-api-key")
    .WithApiSecret("your-api-secret")
    .WithBaseUrl("https://xaman.app/api/v1") // Default
    .WithTimeout(TimeSpan.FromSeconds(30))
    .WithMaxRetries(3)
    .WithRetryDelay(TimeSpan.FromSeconds(1))
    .WithLoggerFactory(loggerFactory) // Optional
    .Build();
```

## Requirements

- .NET 8.0 or higher
- API Key and Secret from the [Xaman Developer Console](https://apps.xaman.dev/)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions, please [open an issue](https://github.com/0xZunia/Xaman.NET.SDK/issues) on GitHub.