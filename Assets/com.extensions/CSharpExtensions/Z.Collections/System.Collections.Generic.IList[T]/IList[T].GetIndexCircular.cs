using System.Collections;
using System.Collections.Generic;

public static partial class Extensions
{
    /// <summary>
    /// Returns the circular next index, this means it will return 0 if you call it for last index of list.
    /// </summary>
    public static int GetNextIndexCircular(this IList list, int currentIndex)
    {
        var count = list.Count;
        return count == 0 ? 0 : (currentIndex + 1) % count;
    }

    /// <summary>
    /// Returns the circular previous index, this means it will return last index of list if you call it for 0
    /// </summary>
    public static int GetPreviousIndexCircular(this IList list, int currentIndex)
    {
        var count = list.Count;
        return count == 0 ? 0 : (currentIndex - 1) % count;
    }
}