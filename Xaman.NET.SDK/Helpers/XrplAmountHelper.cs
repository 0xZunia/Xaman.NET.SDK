using System.Text.Json;

namespace Xaman.NET.SDK.Helpers
{
    /// <summary>
    /// Helper class for working with XRPL currency amounts
    /// </summary>
    public static class XrplAmountHelper
    {
        /// <summary>
        /// Converts a delivered_amount from an XRPL transaction to a decimal value
        /// </summary>
        /// <param name="deliveredAmount">The delivered_amount value from transaction metadata</param>
        /// <returns>A tuple with the decimal amount and currency code</returns>
        public static (decimal Amount, string Currency) ParseAmount(object? deliveredAmount)
        {
            if (deliveredAmount == null)
            {
                return (0, "Unknown");
            }
            
            // If it's a string, it's XRP in drops
            if (deliveredAmount is JsonElement element)
            {
                if (element.ValueKind == JsonValueKind.String)
                {
                    // XRP amount in drops
                    if (decimal.TryParse(element.GetString(), out var dropsAmount))
                    {
                        return (dropsAmount / 1000000m, "XRP");
                    }
                }
                else if (element.ValueKind == JsonValueKind.Object)
                {
                    // IOU amount
                    if (element.TryGetProperty("value", out var value) && 
                        element.TryGetProperty("currency", out var currency))
                    {
                        if (decimal.TryParse(value.GetString(), out var amount))
                        {
                            return (amount, currency.GetString() ?? "Unknown");
                        }
                    }
                }
            }
            
            return (0, "Unknown");
        }
    }
}