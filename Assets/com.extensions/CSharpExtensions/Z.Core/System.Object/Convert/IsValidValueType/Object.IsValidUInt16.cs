// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright ? ZZZ Projects Inc. All rights reserved.
public static partial class Extensions
{
    /// <summary>
    ///     An object extension method that query if '@this' is valid ushort.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid ushort, false if not.</returns>
    public static bool IsValidUInt16(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        ushort result;
        return ushort.TryParse(@this.ToString(), out result);
    }
}