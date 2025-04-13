using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Xaman.NET.SDK.Helpers;

/// <summary>
/// Helper class for working with enumerations
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Gets an enum value from its display name or field name
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="name">The display name or field name</param>
    /// <returns>The enum value</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the name is not found</exception>
    public static T GetValueFromName<T>(string name) where T : Enum
    {
        var type = typeof(T);
        foreach (var field in type.GetFields())
        {
            var attribute = field.GetCustomAttribute<DisplayAttribute>();
            if (attribute?.Name == name || field.Name == name)
            {
                if (field.GetValue(null) is T value)
                {
                    return value;
                }
            }
        }
        
        throw new ArgumentOutOfRangeException(nameof(name), name, $"Specified name was not found in enum {typeof(T).Name}");
    }
    
    /// <summary>
    /// Gets the display name of an enum value
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">The enum value</param>
    /// <returns>The display name or field name if no display attribute is found</returns>
    public static string GetName<T>(T value) where T : Enum
    {
        var field = typeof(T).GetField(value.ToString());
        if (field == null)
        {
            return value.ToString();
        }
        
        var attribute = field.GetCustomAttribute<DisplayAttribute>();
        return attribute?.Name ?? field.Name;
    }
}