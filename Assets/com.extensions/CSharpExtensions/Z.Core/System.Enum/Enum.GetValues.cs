// Description: C# Extension Methods | Enhance the .NET Framework and .NET Core with over 1000 extension methods.
// Website & Documentation: https://csharp-extension.com/
// Issues: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License (MIT): https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: https://zzzprojects.com/
// Copyright © ZZZ Projects Inc. All rights reserved.
using System;

public static partial class Extensions
{
    /// <summary>
    /// Returns an array containing all values of <typeparamref name="T"/>.
    /// </summary>
    public static T[] GetValues<T>() where T : struct, IComparable, IConvertible, IFormattable
    {
        return (T[])Enum.GetValues(typeof(T));
    }
}