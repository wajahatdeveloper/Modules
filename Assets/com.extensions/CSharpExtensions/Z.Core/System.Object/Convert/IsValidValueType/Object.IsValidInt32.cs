// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright ? ZZZ Projects Inc. All rights reserved.
public static partial class Extensions
{
    /// <summary>
    ///     An object extension method that query if '@this' is valid int.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <returns>true if valid int, false if not.</returns>
    public static bool IsValidInt32(this object @this)
    {
        if (@this == null)
        {
            return true;
        }

        int result;
        return int.TryParse(@this.ToString(), out result);
    }
}