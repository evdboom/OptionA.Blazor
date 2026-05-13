namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Shared type-coercion logic for converting raw string values to typed objects.
/// Supported types: <see cref="string"/>, <see cref="bool"/>, <see cref="int"/>, and any <see cref="Enum"/>.
/// </summary>
internal static class TypeCoercionHelper
{
    /// <summary>
    /// Attempts to coerce <paramref name="rawValue"/> to <paramref name="targetType"/>.
    /// When <paramref name="rawValue"/> is <c>null</c> and the underlying type is
    /// <see cref="bool"/>, returns <c>true</c> (boolean shorthand for attribute presence).
    /// Unwraps <see cref="Nullable{T}"/> before coercion.
    /// Returns <c>null</c> on failure or when the type is unsupported.
    /// </summary>
    internal static object? TryCoerce(Type targetType, string? rawValue)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (rawValue is null)
        {
            return underlyingType == typeof(bool) ? (object)true : null;
        }

        return CoerceNonNull(underlyingType, rawValue);
    }

    /// <summary>
    /// Coerces a non-null <paramref name="rawValue"/> to <paramref name="targetType"/>.
    /// Unwraps <see cref="Nullable{T}"/> before coercion.
    /// Falls back to returning the original string only when <paramref name="targetType"/> is
    /// (or unwraps to) <see cref="string"/>; returns the original string on parse failure
    /// for other string-like scenarios but returns <c>null</c> for other type mismatches.
    /// </summary>
    internal static object CoerceWithFallback(Type targetType, string rawValue)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        return CoerceNonNull(underlyingType, rawValue) ?? rawValue;
    }

    private static object? CoerceNonNull(Type targetType, string rawValue)
    {
        if (targetType == typeof(string))
        {
            return rawValue;
        }

        if (targetType == typeof(bool) && bool.TryParse(rawValue, out var b))
        {
            return b;
        }

        if (targetType == typeof(int) && int.TryParse(rawValue, out var i))
        {
            return i;
        }

        if (targetType.IsEnum && Enum.TryParse(targetType, rawValue, ignoreCase: true, out var e))
        {
            return e;
        }

        return null;
    }
}
