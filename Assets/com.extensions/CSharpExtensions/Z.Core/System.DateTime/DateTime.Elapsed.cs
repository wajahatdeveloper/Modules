// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright ? ZZZ Projects Inc. All rights reserved.
using System;

public static partial class Extensions
{
    /// <summary>
    ///     A DateTime extension method that elapsed the given datetime.
    /// </summary>
    /// <param name="datetime">The datetime to act on.</param>
    /// <returns>A TimeSpan.</returns>
    public static TimeSpan Elapsed(this DateTime datetime)
    {
        return DateTime.Now - datetime;
    }
}