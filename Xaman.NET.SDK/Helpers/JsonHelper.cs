using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xaman.NET.SDK.Helpers;

/// <summary>
/// Helper class for JSON serialization and deserialization
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Default JSON serializer options
    /// </summary>
    public static JsonSerializerOptions SerializerOptions => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
    
    /// <summary>
    /// JSON serializer options with indented formatting
    /// </summary>
    public static JsonSerializerOptions PrettySerializerOptions => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    
    /// <summary>
    /// Serializes an object to a JSON string
    /// </summary>
    /// <param name="obj">The object to serialize</param>
    /// <param name="pretty">Whether to format the JSON with indentation</param>
    /// <returns>The JSON string</returns>
    public static string Serialize(object obj, bool pretty = false)
    {
        return JsonSerializer.Serialize(obj, pretty ? PrettySerializerOptions : SerializerOptions);
    }
    
    /// <summary>
    /// Deserializes a JSON string to an object
    /// </summary>
    /// <typeparam name="T">The type to deserialize to</typeparam>
    /// <param name="json">The JSON string</param>
    /// <returns>The deserialized object</returns>
    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, SerializerOptions);
    }
    
    /// <summary>
    /// Deserializes a JSON string to a JsonDocument
    /// </summary>
    /// <param name="json">The JSON string</param>
    /// <returns>The JsonDocument</returns>
    public static JsonDocument ParseJsonDocument(string json)
    {
        return JsonDocument.Parse(json);
    }
}