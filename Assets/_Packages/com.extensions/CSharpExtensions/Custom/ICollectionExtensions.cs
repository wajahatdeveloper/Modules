using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ICollectionExtensions
{
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes the range if contains.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void RemoveRangeIfContains<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            if (@this.Contains(value))
            {
                @this.Remove(value);
            }
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes value that satisfy the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    public static void RemoveWhere<T>(this ICollection<T> @this, Func<T, bool> predicate)
    {
        List<T> list = @this.Where(predicate).ToList();
        foreach (T item in list)
        {
            @this.Remove(item);
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes range item that satisfy the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void RemoveRangeIf<T>(this ICollection<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (T value in values)
        {
            if (predicate(value))
            {
                @this.Remove(value);
            }
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes the range.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void RemoveRange<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            @this.Remove(value);
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes if contains.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    public static void RemoveIfContains<T>(this ICollection<T> @this, T value)
    {
        if (@this.Contains(value))
        {
            @this.Remove(value);
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes if.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <param name="predicate">The predicate.</param>
    public static void RemoveIf<T>(this ICollection<T> @this, T value, Func<T, bool> predicate)
    {
        if (predicate(value))
        {
            @this.Remove(value);
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that query if '@this' contains any value.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool ContainsAny<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            if (@this.Contains(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that query if '@this' contains all values.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool ContainsAll<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            if (!@this.Contains(value))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds a range of values that's not already in the ICollection.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void AddRangeIfNotContains<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            if (!@this.Contains(value))
            {
                @this.Add(value);
            }
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds a collection of objects to the end of this collection only
    ///     for value who satisfies the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void AddRangeIf<T>(this ICollection<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (T value in values)
        {
            if (predicate(value))
            {
                @this.Add(value);
            }
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds a range to 'values'.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void AddRange<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T value in values)
        {
            @this.Add(value);
        }
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that add value if the ICollection doesn't contains it already.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContains<T>(this ICollection<T> @this, T value)
    {
        if (!@this.Contains(value))
        {
            @this.Add(value);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds only if the value satisfies the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIf<T>(this ICollection<T> @this, Func<T, bool> predicate, T value)
    {
        if (predicate(value))
        {
            @this.Add(value);
            return true;
        }

        return false;
    }
}