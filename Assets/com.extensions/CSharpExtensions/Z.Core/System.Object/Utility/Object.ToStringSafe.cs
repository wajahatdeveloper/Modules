// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright ? ZZZ Projects Inc. All rights reserved.
public static partial class Extensions
{
    /// <summary>
    ///     An object extension method that converts the @this to string or return an empty string if the value is null.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>@this as a string or empty if the value is null.</returns>
    public static string ToStringSafe(this object @this)
    {
        return @this == null ? "" : @this.ToString();
    }
}